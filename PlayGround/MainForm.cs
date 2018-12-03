//
//  Program: Playground
//   Author: Joseph Booth
//  Project: Natural Language Processing Succinctly
//  
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TennisData;

/// <summary>
/// Simple front-end to test NLP operations
/// </summary>

namespace PlayGround
{
    public partial class MainForm : Form
    {

        public int TourneyYear = 0;
        public string Tourney = "";
        public const int INTERNAL_CALLS = 0;
        public const int GOOGLE_API = 1;
        public const int CLOUDMERSIVE_API = 2;
        public const int MICROSOFT_API = 3;
        public MainForm()
        {
            InitializeComponent();
            APISourceCombo.SelectedIndex = 0;
        }

        /// <summary>
        /// Enable buttons when text is updated
        /// </summary>
        private void InputText_TextChanged(object sender, EventArgs e)
        {
            BTNSentences.Enabled = InputText.Text.Length > 0;
            BTNWords.Enabled = InputText.Text.Length > 0;
            BTNTags.Enabled = InputText.Text.Length > 0;
            BTNEntities.Enabled = InputText.Text.Length > 0;
            BTNQuery.Enabled = InputText.Text.Length > 0;
        }

        /// <summary>
        /// Parse text into sentences
        /// </summary>
        private void BTNSentences_Click(object sender, EventArgs e)
        {
            List<string> Sentences = new List<string>();
            if (APISourceCombo.SelectedIndex == INTERNAL_CALLS)    { Sentences = NaturalLanguageProcessing.NLP.ExtractSentences(InputText.Text);    }
            if (APISourceCombo.SelectedIndex == GOOGLE_API)        { Sentences = NaturalLanguageProcessing.GoogleNLP.GoogleExtractSentences(InputText.Text);   }
            if (APISourceCombo.SelectedIndex == CLOUDMERSIVE_API)  { Sentences = NaturalLanguageProcessing.CloudmersiveNLP.ExtractSentences(InputText.Text); }

            int SentenceCt = 0;
            RTBox.Clear();
            Color RowColor = Color.Navy;
            for(int x=0;x<Sentences.Count;x++)
            {
                if (!string.IsNullOrWhiteSpace(Sentences[x]))
                {
                    RowColor = Color.Navy;
                    if (SentenceCt % 2 == 0) { RowColor = Color.DarkGreen; }
                    AppendText(RTBox, "[" + (SentenceCt + 1).ToString() + "] " + Sentences[x].Trim(), RowColor, true);
                    SentenceCt++;
                }
            }

        }

        /// <summary>
        /// Extract word objects from sentence
        /// </summary>
        private void BTNWords_Click(object sender, EventArgs e)
        {
            int wordCt = 0;
            var words_ = NaturalLanguageProcessing.NLP.ExtractWords(InputText.Text);
            RTBox.Clear();
            Color RowColor = Color.Navy;
            for (int x = 0; x < words_.Count; x++) 
            {
                if (!string.IsNullOrWhiteSpace(words_[x]))
                {
                    wordCt++;
                    RowColor = Color.Navy;
                    if (wordCt% 2 == 0) { RowColor = Color.DarkGreen; }
                    AppendText(RTBox, "[" + (wordCt).ToString() + "] " + words_[x].Trim(), RowColor, true);
                }
            }
        }
        /// <summary>
        ///  Show tags from sentence
        /// </summary>
        private void BTNTags_Click(object sender, EventArgs e)
        {
            int x = 0;
            var words_ = NaturalLanguageProcessing.NLP.ExtractWords(InputText.Text);
            List<string> Tags = new List<string>();
            foreach (string curWord in words_)
            {
                string GuessTag = NaturalLanguageProcessing.Tagger.TagWord(curWord);
                Tags.Add(GuessTag);
            }
            List<string> revised = NaturalLanguageProcessing.Tagger.RevisedTags(words_, Tags);

            RTBox.Clear();
            Color RowColor = Color.Navy;

            for (x = 0; x < revised.Count; x++)
            {
                RowColor = Color.Black;
                if (revised[x].StartsWith("VB") ) { RowColor = Color.ForestGreen;  }
                if (revised[x].StartsWith("JJ") ) { RowColor = Color.Navy; }
                AppendText( RTBox,( (x + 1).ToString() + ".  [" + (revised[x] + "]")).PadRight(12) + "  " + words_[x].Trim().ToString(),RowColor,true);
            }
        }
        /// <summary>
        /// Extract named entities from text
        /// </summary>
        private void BTNEntities_Click(object sender, EventArgs e)
        {
            //
            var words_ = NaturalLanguageProcessing.NLP.ExtractWords(InputText.Text);
            List<string> Tags = new List<string>();
            foreach (string curWord in words_)
            {
                string GuessTag = NaturalLanguageProcessing.Tagger.TagWord(curWord);
                Tags.Add(GuessTag);
            }
            List<string> revised = NaturalLanguageProcessing.Tagger.RevisedTags(words_, Tags);
            NaturalLanguageProcessing.Entities.UpdateNamedEntities(words_, Tags);
            RTBox.Clear();
            Color RowColor = Color.Navy;

            for (int x = 0; x < revised.Count; x++)
            {
                RowColor = Color.Black;
                AppendText(RTBox, ((x + 1).ToString() + ".  [" + (Tags[x] + "]")).PadRight(12) + "  " + words_[x].Trim().ToString(), RowColor, true);
            }
        }

        /// <summary>
        /// Attempt to answer question
        /// </summary>
        private void BTNQuery_Click(object sender, EventArgs e)
        {
            var words_ = NaturalLanguageProcessing.NLP.ExtractWords(InputText.Text);
            List<string> Tags = new List<string>();
            foreach (string curWord in words_)
            {
                string GuessTag = NaturalLanguageProcessing.Tagger.TagWord(curWord);
                Tags.Add(GuessTag);
            }
            List<string> revised = NaturalLanguageProcessing.Tagger.RevisedTags(words_, Tags);
            NaturalLanguageProcessing.Entities.UpdateNamedEntities(words_, Tags);


            AppendText(RTBox, InputText.Text.ToUpper(), Color.Blue, true);

            InputText.Text = "";
            string ans = TennisMajors.GetResponse(words_, Tags);

            if (ans.Length>0)
            {
                AppendText(RTBox,ans, Color.Red,false);
            }
            AppendText(RTBox, " ", Color.Blue, true);

        }

        /// <summary>
        /// Add color-coded lines to text box
        /// </summary>
        private void AppendText(RichTextBox box, string text, Color color, bool AddNewLine = false)
        {
            if (AddNewLine)
            {
                text += Environment.NewLine;
            }
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

    }
}
