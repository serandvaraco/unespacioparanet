namespace BotBuilderDialogs
{
    using BotBuilderDialogs.Contracts;
    using Microsoft.Bot.Connector;
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using System.Web.Hosting;

    [Serializable]
    public sealed class SurveyScheduler : ISurveyScheduler
    {
        private readonly ConcurrentQueue<ConversationReference> surveyRequests = new ConcurrentQueue<ConversationReference>();

        public SurveyScheduler()
        {
            HostingEnvironment.QueueBackgroundWorkItem(async token =>
            {
                while (true)
                {
                    token.ThrowIfCancellationRequested();

                    while (surveyRequests.Count > 0)
                    {
                        ConversationReference surveyRequest = null;

                        if (surveyRequests.TryDequeue(out surveyRequest))
                        {
                            await SurveyTriggerer.StartSurvey(surveyRequest, token);
                        }
                    }

                    // polling is one of the naive aspects of this implementation
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            });
        }

        public void Add(ConversationReference conversationReference)
        {
            this.surveyRequests.Enqueue(conversationReference);
        }
    }
}
