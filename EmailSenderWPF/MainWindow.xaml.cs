using EmailSenderWPF.Models;
using Microsoft.Win32;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Documents;

namespace EmailSenderWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Sender mailSender;
    private string jsonContent;
    private List<string> emails;

    public MainWindow()
    {
        InitializeComponent();
        mailSender = new Sender();
        HostBox.Text = "smtp.gmail.com";
        PortBox.Text = "587";
    }

    private void ChooseFile_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";

        if (openFileDialog.ShowDialog() == true)
        {
            FilePathTextBox.Text = openFileDialog.FileName;
            string jsonFilePath = openFileDialog.FileName;
            jsonContent = File.ReadAllText(jsonFilePath);
            var data = JsonSerializer.Deserialize<Recipient>(jsonContent);
            emails = data.Emails;
        }
    }

    private void SendEmail_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string fromEmail = SenderEmailTextBox.Text;
            string password = PasswordBox.Password;
            string host = HostBox.Text;
            string subject = SubjectBox.Text;
            string senderName = SenderNameBox.Text;
            string messageBody = MessageRichTextBox.Text;
            if (int.TryParse(PortBox.Text, out var port))
            {
                mailSender.Port = port;
                mailSender.Host = host;
                mailSender.Password = password;
                mailSender.Subject = subject;
                mailSender.SenderName = senderName;
                mailSender.SenderEmail = fromEmail;
                mailSender.MessageBody = messageBody;
            }

            foreach (var email in emails)
            {
                mailSender.SendMail(email);
            }

            MessageBox.Show("Emails sent successfully.");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }
}