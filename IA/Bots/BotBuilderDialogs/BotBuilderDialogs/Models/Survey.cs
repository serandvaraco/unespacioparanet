﻿using Microsoft.Bot.Builder.FormFlow;
using System;

namespace BotBuilderDialogs.Models
{
    [Serializable]
    public class Survey
    {
        [Prompt("Hello... What's your name?")]
        public string Name { get; set; }
        [Prompt("How many years have you been coding?")]
        public int YearsCoding { get; set; }
        [Prompt("What's your preferred programming language?")]
        public string Language { get; set; }
    }
}