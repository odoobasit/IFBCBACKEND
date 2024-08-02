using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;

public class OtpService
{
    private Dictionary<string, (string otp, DateTime expiry)> _otpDictionary = new Dictionary<string, (string otp, DateTime expiry)>();

    private void SendOTP(string toEmail, string name, string otp)
    {
        var fromAddress = new MailAddress("charliechaplin565@gmail.com", "IFBC");
        var toAddress = new MailAddress(toEmail, name);
        const string fromPassword = "nvtv cbdb vhsj evjf"; // This should be securely stored
        const string subject = "Your OTP Code";
        string body = $"Your OTP for registration is: {otp}";

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };
        using (var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        })
        {
            smtp.Send(message);
        }
        Console.WriteLine($"OTP {otp} sent to {toEmail}");
    }

    // Method to generate and send OTP
    public void GenerateAndSendOtp(string userEmail, string name)
    {
        // Generate a random OTP
        string otp = GenerateOTP();
        Console.WriteLine($"Generated OTP: {otp} for {userEmail}");

        // Store OTP with expiry time (e.g., 5 minutes)
        _otpDictionary[userEmail] = (otp, DateTime.Now.AddMinutes(5));
        Console.WriteLine($"Stored OTP for {userEmail} with expiry at {DateTime.Now.AddMinutes(5)}");

        // Send OTP to user
        SendOTP(userEmail, name, otp);
    }

    // Method to verify OTP
    public bool VerifyOtp(string userEmail, string userOtp)
    {
        // Check if OTP exists for the user
        if (_otpDictionary.TryGetValue(userEmail, out var otpDetails))
        {
            // Check if OTP is correct and not expired
            if (otpDetails.otp == userOtp && otpDetails.expiry > DateTime.Now)
            {
                // Remove the OTP from dictionary (OTP can be used only once)
                _otpDictionary.Remove(userEmail);
                Console.WriteLine($"Verified and removed OTP for {userEmail}");
                return true;
            }
            else
            {
                Console.WriteLine($"OTP for {userEmail} is either incorrect or expired.");
            }
        }
        else
        {
            Console.WriteLine($"No OTP found for {userEmail}");
        }

        return false;
    }

    // Method to generate a random OTP
    private string GenerateOTP()
    {
        Random rnd = new Random();
        int otp = rnd.Next(100000, 999999);
        return otp.ToString();
    }
}
