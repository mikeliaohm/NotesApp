using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NotesApp.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        SpeechRecognitionEngine recognizer;

        bool isRecognizing = false;

        public NotesWindow()
        {
            InitializeComponent();

            var currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers()
                                 where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
                                 select r).FirstOrDefault();

            recognizer = new SpeechRecognitionEngine(currentCulture);

            GrammarBuilder builder = new GrammarBuilder();
            builder.AppendDictation();
            Grammar grammer = new Grammar(builder);

            recognizer.LoadGrammar(grammer);
            recognizer.SetInputToDefaultAudioDevice();
            recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void contentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int amountOfCharacters = (new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd)).Text.Length;

            statusTextBlock.Text = $"Document length: {amountOfCharacters} characters";
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
        }

        private void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            if(!isRecognizing)
            {
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
                isRecognizing = true;
            }
            else
            {
                recognizer.RecognizeAsyncStop();
                isRecognizing = false;
            }
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string recognizedText = e.Result.Text;

            contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(recognizedText)));
        }
    }
}
