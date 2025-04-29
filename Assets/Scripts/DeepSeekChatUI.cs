using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;

public class DeepSeekChatUI : MonoBehaviour
{
    [Header("API Configuration")]
    public string apiEndpoint = "https://api.deepseek.com/v1/chat/completions";
    public string apiKey = "your-api-key-here";
    public string model = "deepseek-chat";

    [Header("UI Components")]
    [SerializeField] private TMP_InputField userInputField;
    [SerializeField] private Button sendButton;
    [SerializeField] private TMP_Text chatDisplayText;  // 确保这个组件已正确连接
    [SerializeField] private ScrollRect chatScrollRect;
    [SerializeField] private GameObject loadingIndicator;

    [Header("Chat Settings")]
    public int maxTokens = 2048;
    public float temperature = 0.7f;
    public string systemPrompt = "你是一个乐于助人的AI助手";

    private List<ChatMessage> conversationHistory = new List<ChatMessage>();

    void Start()
    {
        // 初始化检查
        if (!CheckUIComponents()) return;

        sendButton.onClick.AddListener(OnSendButtonClicked);
        userInputField.onEndEdit.AddListener((text) => {
            if (Input.GetKeyDown(KeyCode.Return)) OnSendButtonClicked();
        });

        InitializeConversation();
        UpdateChatDisplay(); // 初始化时更新显示
    }

    private bool CheckUIComponents()
    {
        if (userInputField == null || sendButton == null || chatDisplayText == null)
        {
            Debug.LogError("UI组件未正确分配！");
            enabled = false;
            return false;
        }
        return true;
    }

    private void InitializeConversation()
    {
        conversationHistory.Clear();
        if (!string.IsNullOrEmpty(systemPrompt))
        {
            conversationHistory.Add(new ChatMessage
            {
                role = "system",
                content = systemPrompt
            });
        }
    }

    private void OnSendButtonClicked()
    {
        string userMessage = userInputField.text.Trim();
        if (string.IsNullOrEmpty(userMessage)) return;

        // 添加用户消息
        conversationHistory.Add(new ChatMessage
        {
            role = "user",
            content = userMessage
        });

        UpdateChatDisplay(); // 立即更新显示用户消息
        userInputField.text = "";
        StartCoroutine(SendChatRequest());
    }

    private IEnumerator SendChatRequest()
    {
        SetLoadingState(true);

        // 构建请求数据
        var requestData = new
        {
            model = this.model,
            messages = conversationHistory,
            max_tokens = this.maxTokens,
            temperature = this.temperature,
            stream = false
        };

        string jsonBody = JsonConvert.SerializeObject(requestData);
        Debug.Log("Request: " + jsonBody);

        using (UnityWebRequest request = new UnityWebRequest(apiEndpoint, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {apiKey}");

            yield return request.SendWebRequest();

            SetLoadingState(false);

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + request.downloadHandler.text);
                ProcessApiResponse(request.downloadHandler.text);
            }
            else
            {
                HandleApiError(request);
            }
        }
    }

    private void ProcessApiResponse(string jsonResponse)
    {
        try
        {
            var response = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
            if (response.choices != null && response.choices.Count > 0)
            {
                string assistantReply = response.choices[0].message.content;

                conversationHistory.Add(new ChatMessage
                {
                    role = "assistant",
                    content = assistantReply
                });

                UpdateChatDisplay(); // 收到回复后更新显示
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("解析响应失败: " + e.Message);
            conversationHistory.Add(new ChatMessage
            {
                role = "assistant",
                content = "解析回复时出错"
            });
            UpdateChatDisplay();
        }
    }

    private void HandleApiError(UnityWebRequest request)
    {
        string errorMsg = $"错误: {request.responseCode}";
        if (!string.IsNullOrEmpty(request.error))
        {
            errorMsg += $" - {request.error}";
        }

        Debug.LogError(errorMsg);
        conversationHistory.Add(new ChatMessage
        {
            role = "assistant",
            content = "请求出错，请稍后再试"
        });
        UpdateChatDisplay();
    }

    private void SetLoadingState(bool isLoading)
    {
        sendButton.interactable = !isLoading;
        if (loadingIndicator != null)
            loadingIndicator.SetActive(isLoading);
    }

    // 这是关键修改部分 - 确保正确更新UI显示
    private void UpdateChatDisplay()
    {
        StringBuilder chatContent = new StringBuilder();

        foreach (var message in conversationHistory)
        {
            if (message.role == "system") continue; // 不显示系统消息

            string displayText = message.role == "user"
                ? $"<color=#3498db>你:</color> {message.content}\n\n"
                : $"<color=#2ecc71>AI助手:</color> {message.content}\n\n";

            chatContent.Append(displayText);
        }

        chatDisplayText.text = chatContent.ToString();

        // 强制刷新并滚动到底部
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatScrollRect.content);
        StartCoroutine(ScrollToBottom());
    }

    private IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        chatScrollRect.verticalNormalizedPosition = 0f;
    }

    // 数据结构
    [System.Serializable]
    private class ChatMessage
    {
        public string role;
        public string content;
    }

    [System.Serializable]
    private class ApiResponse
    {
        public List<Choice> choices;
    }

    [System.Serializable]
    private class Choice
    {
        public ChatMessage message;
    }
}