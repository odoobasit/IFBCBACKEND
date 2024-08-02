using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using System.Linq;
using System.Text;
using WebApplication1.Models;

[Route("api/[controller]")]
[ApiController]
public class CheckoutController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CheckoutController(ApplicationDbContext context)
    {
        _context = context;
    }

    private void SendEmail(string body, string email, string name)
    {
        var fromAddress = new MailAddress("sm@ifbc.co", "IFBC");
        var toAddress = new MailAddress(email, name);        
        const string fromPassword = "hhcw vgqc ulsj lfof";
        const string subject = "Explore Your Franchise Opportunity Details—Your Request Is In!";

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
            Body = body,
            IsBodyHtml = true
        })
        {
            smtp.Send(message);
        }
    }

    public static string GenerateFranchiseHtml(List<listingsMstr> franchises)
    {
        var franchisesHtml = new StringBuilder();

        foreach (var franchise in franchises)
        {
            franchisesHtml.Append($@"

    <tr>
        <td
          style=""overflow-wrap: break-word; word-break: break-word; padding: 10px; font-family: arial, helvetica, sans-serif;""
          align=""left""
        >
          <div
            style=""font-family: arial, helvetica, sans-serif; font-size: 15px; line-height: 140%; text-align: left; word-wrap: break-word;""
          >

            <p style=""line-height: 140%"">
              <img src=""https://ifbc.co/{franchise.ImgUrl}"" alt={franchise.Name} width=""60px""/> 
            </p>

          </div>
        </td>

  <td
          style=""overflow-wrap: break-word; word-break: break-word; padding: 10px; font-family: arial, helvetica, sans-serif;""
          align=""left""
        >
          <div
            style=""padding-top:10px;font-family: arial, helvetica, sans-serif; font-size: 15px; line-height: 140%; text-align: left; word-wrap: break-word;""
          >
            <p style=""line-height: 140%"">
             {franchise.Name}
            </p>

          </div>
        </td>

       
    </tr>");
        }

        return franchisesHtml.ToString();
    }

    public static string GenerateHtmlTable(Checkout formData, List<listingsMstr> franchises)
    {
        var properties = typeof(Checkout).GetProperties();
        var htmlBuilder = new StringBuilder();
        string franchiseEmailBody = GenerateFranchiseHtml(franchises);

        foreach (var prop in properties)
        {
            var propName = prop.Name;
            var propValue = prop.GetValue(formData, null)?.ToString() ?? string.Empty;

            if (propName != "DocId" && propName != "NewsLetter" && propName != "DocDate")
            {
                if (propName == "CartListings")
                {
                    htmlBuilder.Append($@"
<h1>Selected Franchises: </h1>
  <table
                    style=""font-family: arial, helvetica, sans-serif""
                    role=""presentation""
                    cellpadding=""0""
                    cellspacing=""0""
                    width=""100%""
                    border=""0""
                >
                    <tbody>
{franchiseEmailBody}

                    </tbody>
                </table>
");
                }
                else
                {
                    htmlBuilder.Append($@"
                <table
                    style=""font-family: arial, helvetica, sans-serif""
                    role=""presentation""
                    cellpadding=""0""
                    cellspacing=""0""
                    width=""100%""
                    border=""0""
                >
                    <tbody>
                        <tr>
                            <td
                                style=""
                                    overflow-wrap: break-word;
                                    word-break: break-word;
                                    padding: 10px;
                                    font-family: arial, helvetica, sans-serif;
                                ""
                                align=""left""
                            >
                                <div
                                    style=""
                                        font-family: arial, helvetica, sans-serif;
                                        font-size: 15px;
                                        line-height: 140%;
                                        text-align: left;
                                        word-wrap: break-word;
                                    ""
                                >
                                    <p style=""line-height: 140%"">{propName}: {propValue}</p>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            ");
                }

            }


        }

        return htmlBuilder.ToString();
    }


    // POST: api/YourTableName
    [HttpPost]
    public async Task<ActionResult<Checkout>> PostYourTableName(Checkout checkout)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _context.Checkout.Add(checkout);
            await _context.SaveChangesAsync();

            string body = $@"
    <!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional //EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
    <html
      xmlns=""http://www.w3.org/1999/xhtml""
      xmlns:v=""urn:schemas-microsoft-com:vml""
      xmlns:o=""urn:schemas-microsoft-com:office:office""
    >
      <head>
        <!--[if gte mso 9]>
          <xml>
            <o:OfficeDocumentSettings>
              <o:AllowPNG />
              <o:PixelsPerInch>96</o:PixelsPerInch>
            </o:OfficeDocumentSettings>
          </xml>
        <![endif]-->
        <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
        <meta name=""x-apple-disable-message-reformatting"" />
        <!--[if !mso]><!-->
        <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" />
        <!--<![endif]-->
        <title></title>

        <style type=""text/css"">
          @media only screen and (min-width: 520px) {{
            .u-row {{
              width: 500px !important;
            }}
            .u-row .u-col {{
              vertical-align: top;
            }}

            .u-row .u-col-100 {{
              width: 500px !important;
            }}
          }}

          @media (max-width: 520px) {{
            .u-row-container {{
              max-width: 100% !important;
              padding-left: 0px !important;
              padding-right: 0px !important;
            }}
            .u-row .u-col {{
              min-width: 320px !important;
              max-width: 100% !important;
              display: block !important;
            }}
            .u-row {{
              width: 100% !important;
            }}
            .u-col {{
              width: 100% !important;
            }}
            .u-col > div {{
              margin: 0 auto;
            }}
          }}
          body {{
            margin: 0;
            padding: 0;
          }}

          table,
          tr,
          td {{
            vertical-align: top;
            border-collapse: collapse;
          }}

          p {{
            margin: 0;
          }}

          .ie-container table,
          .mso-container table {{
            table-layout: fixed;
          }}

          * {{
            line-height: inherit;
          }}

          a[x-apple-data-detectors=""true""] {{
            color: inherit !important;
            text-decoration: none !important;
          }}

          table,
          td {{
            color: #000000;
          }}
          #u_body a {{
            color: #0000ee;
            text-decoration: underline;
          }}
          @media (max-width: 480px) {{
            #u_content_image_1 .v-src-width {{
              width: auto !important;
            }}
            #u_content_image_1 .v-src-max-width {{
              max-width: 75% !important;
            }}
          }}
        </style>
      </head>

      <body
        class=""clean-body u_body""
        style=""
          margin: 0;
          padding: 0;
          -webkit-text-size-adjust: 100%;
          background-color: #f7f8f9;
          color: #000000;
        ""
      >
        <!--[if IE]><div class=""ie-container""><![endif]-->
        <!--[if mso]><div class=""mso-container""><![endif]-->
        <table
          id=""u_body""
          style=""
            border-collapse: collapse;
            table-layout: fixed;
            border-spacing: 0;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            vertical-align: top;
            min-width: 320px;
            margin: 0 auto;
            background-color: #f7f8f9;
            width: 100%;
          ""
          cellpadding=""0""
          cellspacing=""0""
        >
          <tbody>
            <tr style=""vertical-align: top"">
              <td
                style=""
                  word-break: break-word;
                  border-collapse: collapse !important;
                  vertical-align: top;
                ""
              >
                <!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td align=""center"" style=""background-color: #F7F8F9;""><![endif]-->

                <div
                  class=""u-row-container""
                  style=""padding: 0px; background-color: transparent""
                >
                  <div
                    class=""u-row""
                    style=""
                      margin: 0 auto;
                      min-width: 320px;
                      max-width: 500px;
                      overflow-wrap: break-word;
                      word-wrap: break-word;
                      word-break: break-word;
                      background-color: transparent;
                    ""
                  >
                    <div
                      style=""
                        border-collapse: collapse;
                        display: table;
                        width: 100%;
                        height: 100%;
                        background-color: transparent;
                      ""
                    >
                      <!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding: 0px;background-color: transparent;"" align=""center""><table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""width:500px;""><tr style=""background-color: transparent;""><![endif]-->

                      <!--[if (mso)|(IE)]><td align=""center"" width=""500"" style=""background-color: #84abff;width: 500px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"" valign=""top""><![endif]-->
                      <div
                        class=""u-col u-col-100""
                        style=""
                          max-width: 320px;
                          min-width: 500px;
                          display: table-cell;
                          vertical-align: top;
                        ""
                      >
                        <div
                          style=""
                            background-color: #84abff;
                            height: 100%;
                            width: 100% !important;
                            border-radius: 0px;
                            -webkit-border-radius: 0px;
                            -moz-border-radius: 0px;
                          ""
                        >
                          <!--[if (!mso)&(!IE)]><!--><div
                            style=""
                              box-sizing: border-box;
                              height: 100%;
                              padding: 0px;
                              border-top: 0px solid transparent;
                              border-left: 0px solid transparent;
                              border-right: 0px solid transparent;
                              border-bottom: 0px solid transparent;
                              border-radius: 0px;
                              -webkit-border-radius: 0px;
                              -moz-border-radius: 0px;
                            ""
                          ><!--<![endif]-->
                            <table
                              id=""u_content_image_1""
                              style=""font-family: arial, helvetica, sans-serif""
                              role=""presentation""
                              cellpadding=""0""
                              cellspacing=""0""
                              width=""100%""
                              border=""0""
                            >
                              <tbody>
                                <tr>
                                  <td
                                    style=""
                                      overflow-wrap: break-word;
                                      word-break: break-word;
                                      padding: 10px;
                                      font-family: arial, helvetica, sans-serif;
                                    ""
                                    align=""left""
                                  >
                                    <table
                                      width=""100%""
                                      cellpadding=""0""
                                      cellspacing=""0""
                                      border=""0""
                                    >
                                      <tr>
                                        <td
                                          style=""
                                            padding-right: 0px;
                                            padding-left: 0px;
                                          ""
                                          align=""center""
                                        >
                                          <a
                                            href=""https://ifbc.co/""
                                            target=""_blank""
                                          >
                                            <img
                                              align=""center""
                                              border=""0""
                                              src=""https://ifbc.co/images/logo/IFBC%203.png""
                                              alt=""""
                                              title=""""
                                              style=""
                                                outline: none;
                                                text-decoration: none;
                                                -ms-interpolation-mode: bicubic;
                                                clear: both;
                                                display: inline-block !important;
                                                border: none;
                                                height: auto;
                                                float: none;
                                                width: 60%;
                                                max-width: 288px;
                                              ""
                                              width=""288""
                                              class=""v-src-width v-src-max-width""
                                            />
                                          </a>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                              </tbody>
                            </table>

 <table
                          style=""font-family: arial, helvetica, sans-serif""
                          role=""presentation""
                          cellpadding=""0""
                          cellspacing=""0""
                          width=""100%""
                          border=""0""
                        >
                          <tbody>
                            <tr>
                              <td
                                style=""
                                  overflow-wrap: break-word;
                                  word-break: break-word;
                                  padding: 10px;
                                  font-family: arial, helvetica, sans-serif;
                                ""
                                align=""left""
                              >
                                <!--[if mso]><table width=""100%""><tr><td><![endif]-->
                                <h1
                                  style=""
                                    margin: 0px;
                                    color: #ffffff;
                                    line-height: 140%;
                                    text-align: center;
                                    word-wrap: break-word;
                                    font-family: arial, helvetica, sans-serif;
                                    font-size: 22px;
                                    font-weight: 400;
                                  ""
                                >
                                  <p>Thank you for reaching out to International Franchise Business Consultant Corp</p>
                                </h1>
                                <!--[if mso]></td></tr></table><![endif]-->
                              </td>
                            </tr>
                          </tbody>
                        </table>

                            <!--[if (!mso)&(!IE)]><!-->
                          </div>
                          <!--<![endif]-->
                        </div>
                      </div>
                      <!--[if (mso)|(IE)]></td><![endif]-->
                      <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->
                    </div>
                  </div>
                </div>

                <div
                  class=""u-row-container""
                  style=""padding: 0px; background-color: transparent""
                >
                  <div
                    class=""u-row""
                    style=""
                      margin: 0 auto;
                      min-width: 320px;
                      max-width: 500px;
                      overflow-wrap: break-word;
                      word-wrap: break-word;
                      word-break: break-word;
                      background-color: transparent;
                    ""
                  >
                    <div
                      style=""
                        border-collapse: collapse;
                        display: table;
                        width: 100%;
                        height: 100%;
                        background-color: transparent;
                      ""
                    >
                      <!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding: 0px;background-color: transparent;"" align=""center""><table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""width:500px;""><tr style=""background-color: transparent;""><![endif]-->

                      <!--[if (mso)|(IE)]><td align=""center"" width=""500"" style=""background-color: #ededed;width: 500px;padding: 11px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"" valign=""top""><![endif]-->
                      <div
                        class=""u-col u-col-100""
                        style=""
                          max-width: 320px;
                          min-width: 500px;
                          display: table-cell;
                          vertical-align: top;
                        ""
                      >
                        <div
                          style=""
                            background-color: #ededed;
                            height: 100%;
                            width: 100% !important;
                            border-radius: 0px;
                            -webkit-border-radius: 0px;
                            -moz-border-radius: 0px;
                          ""
                        >
                          <!--[if (!mso)&(!IE)]><!--><div
                            style=""
                              box-sizing: border-box;
                              height: 100%;
                              padding: 11px;
                              border-top: 0px solid transparent;
                              border-left: 0px solid transparent;
                              border-right: 0px solid transparent;
                              border-bottom: 0px solid transparent;
                              border-radius: 0px;
                              -webkit-border-radius: 0px;
                              -moz-border-radius: 0px;
                            ""
                          ><!--<![endif]-->
                            <table
                              style=""font-family: arial, helvetica, sans-serif""
                              role=""presentation""
                              cellpadding=""0""
                              cellspacing=""0""
                              width=""100%""
                              border=""0""
                            >
                              <tbody>
                                <tr>
                                  <td
                                    style=""
                                      overflow-wrap: break-word;
                                      word-break: break-word;
                                      padding: 10px;
                                      font-family: arial, helvetica, sans-serif;
                                    ""
                                    align=""left""
                                  >
                                    <div
                                      style=""
                                        font-family: arial, helvetica, sans-serif;
                                        font-size: 15px;
                                        color: #000000;
                                        line-height: 140%;
                                        text-align: left;
                                        word-wrap: break-word;
                                      ""
                                    >
                                      <p style=""line-height: 140%"">
                                       Dear {checkout.Name},
<br/>
We appreciate your interest in our services and are excited about the opportunity to assist you in your franchise journey.
<br/>
Our team is dedicated to providing you with expert guidance and support to help you achieve your business goals. Whether you are looking to start a new franchise, expand an existing one, or need consultation on any aspect of your franchise business, we are here to help.
<br/>
We will review your inquiry and get back to you as soon as possible. In the meantime, feel free to explore our website for more information about our services and resources <a href=""https://ifbc.co/listings"">here</a>
<br/>
If you have any immediate questions or need further assistance, please do not hesitate to contact us at <a href=""tel:914-357-4322"">91-HELP-IFBC</a> 
<br/>
If you want to be a part of the IFBC team you can contact us at <a href=""tel:908-326-4322"">90-TEAM-IFBC</a>
<br/>
Thank you once again for choosing <a href=""https://ifbc.co"">International Franchise Business Consultant Corp</a>. We look forward to working with you.
<br/>
Best regards,
<br/>
Team IFBC<br/>
Email us at <a href=""mailto:info@ifbc.co"">info@ifbc.co</a><br/>
                                      </p>
                                    </div>
                                  </td>
                                </tr>
                              </tbody>
                            </table>

                            <!--[if (!mso)&(!IE)]><!-->
                          </div>
                          <!--<![endif]-->
                        </div>
                      </div>
                      <!--[if (mso)|(IE)]></td><![endif]-->
                      <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->
                    </div>
                  </div>
                </div>

                <div
                  class=""u-row-container""
                  style=""padding: 0px; background-color: transparent""
                >
                  <div
                    class=""u-row""
                    style=""
                      margin: 0 auto;
                      min-width: 320px;
                      max-width: 500px;
                      overflow-wrap: break-word;
                      word-wrap: break-word;
                      word-break: break-word;
                      background-color: transparent;
                    ""
                  >
                    <div
                      style=""
                        border-collapse: collapse;
                        display: table;
                        width: 100%;
                        height: 100%;
                        background-color: transparent;
                      ""
                    >
                      <!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding: 0px;background-color: transparent;"" align=""center""><table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""width:500px;""><tr style=""background-color: transparent;""><![endif]-->

                      <!--[if (mso)|(IE)]><td align=""center"" width=""500"" style=""background-color: #ededed;width: 500px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"" valign=""top""><![endif]-->
                      <div
                        class=""u-col u-col-100""
                        style=""
                          max-width: 320px;
                          min-width: 500px;
                          display: table-cell;
                          vertical-align: top;
                        ""
                      >
                        <div
                          style=""
                            background-color: #ededed;
                            height: 100%;
                            width: 100% !important;
                            border-radius: 0px;
                            -webkit-border-radius: 0px;
                            -moz-border-radius: 0px;
                          ""
                        >
                          <!--[if (!mso)&(!IE)]><!--><div
                            style=""
                              box-sizing: border-box;
                              height: 100%;
                              padding: 0px;
                              border-top: 0px solid transparent;
                              border-left: 0px solid transparent;
                              border-right: 0px solid transparent;
                              border-bottom: 0px solid transparent;
                              border-radius: 0px;
                              -webkit-border-radius: 0px;
                              -moz-border-radius: 0px;
                            ""
                          ><!--<![endif]-->
                            <table
                              style=""font-family: arial, helvetica, sans-serif""
                              role=""presentation""
                              cellpadding=""0""
                              cellspacing=""0""
                              width=""100%""
                              border=""0""
                            >
                              <tbody>
                                <tr>
                                  <td
                                    style=""
                                      overflow-wrap: break-word;
                                      word-break: break-word;
                                      padding: 10px;
                                      font-family: arial, helvetica, sans-serif;
                                    ""
                                    align=""left""
                                  >
                                    <div class=""menu"" style=""text-align: center"">
                                      <!--[if (mso)|(IE)]><table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center""><tr><![endif]-->

                                      <!--[if (mso)|(IE)]><td style=""padding:5px 15px""><![endif]-->

                                      <a
                                        href=""https://ifbc.co/privacy-policy""
                                        target=""_blank""
                                        style=""
                                          padding: 5px 15px;
                                          display: inline-block;
                                          color: #4d4d4d;
                                          font-family: arial, helvetica, sans-serif;
                                          font-size: 12px;
                                          text-decoration: none;
                                        ""
                                      >
                                        Privacy policy
                                      </a>

                                      <!--[if (mso)|(IE)]></td><![endif]-->

                                      <!--[if (mso)|(IE)]><td style=""padding:5px 15px""><![endif]-->

                                      <a
                                        href=""https://ifbc.co/terms-conditions""
                                        target=""_self""
                                        style=""
                                          padding: 5px 15px;
                                          display: inline-block;
                                          color: #4d4d4d;
                                          font-family: arial, helvetica, sans-serif;
                                          font-size: 12px;
                                          text-decoration: none;
                                        ""
                                      >
                                        Terms & Conditions
                                      </a>

                                      <!--[if (mso)|(IE)]></td><![endif]-->

                                      <!--[if (mso)|(IE)]></tr></table><![endif]-->
                                    </div>
                                  </td>
                                </tr>
                              </tbody>
                            </table>

                            <table
                              style=""font-family: arial, helvetica, sans-serif""
                              role=""presentation""
                              cellpadding=""0""
                              cellspacing=""0""
                              width=""100%""
                              border=""0""
                            >
                              <tbody>
                                <tr>
                                  <td
                                    style=""
                                      overflow-wrap: break-word;
                                      word-break: break-word;
                                      padding: 10px;
                                      font-family: arial, helvetica, sans-serif;
                                    ""
                                    align=""left""
                                  >
                                    <div
                                      style=""
                                        font-family: arial, helvetica, sans-serif;
                                        font-size: 10px;
                                        line-height: 140%;
                                        text-align: center;
                                        word-wrap: break-word;
                                      ""
                                    >
                                      <p style=""line-height: 140%"">
                                        2024 - Powered by International Franchise
                                        Business Consultant Corp. 
                                      </p>
                                    </div>
                                  </td>
                                </tr>
                              </tbody>
                            </table>

                            <!--[if (!mso)&(!IE)]><!-->
                          </div>
                          <!--<![endif]-->
                        </div>
                      </div>
                      <!--[if (mso)|(IE)]></td><![endif]-->
                      <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->
                    </div>
                  </div>
                </div>

                <!--[if (mso)|(IE)]></td></tr></table><![endif]-->
              </td>
            </tr>
          </tbody>
        </table>
        <!--[if mso]></div><![endif]-->
        <!--[if IE]></div><![endif]-->
      </body>
    </html>

                        ";
            SendEmail(body, checkout.Email, checkout.Name);
            // Ensure the SendEmail method is functioning correctly

           int[] listingsArray = checkout.CartListings.TrimStart('[').TrimEnd(']')
                       .Split(',')
                       .Select(int.Parse)
                       .ToArray();
            var franchises = await _context.listingsMstr
                             .Where(f => listingsArray.Contains(f.DocId ?? 0))  // Handle null DocId
                             .ToListAsync();

            string emailBody = GenerateHtmlTable(checkout, franchises);

            body = $@"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional //EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html
  xmlns=""http://www.w3.org/1999/xhtml""
  xmlns:v=""urn:schemas-microsoft-com:vml""
  xmlns:o=""urn:schemas-microsoft-com:office:office""
>
  <head>
    <!--[if gte mso 9]>
      <xml>
        <o:OfficeDocumentSettings>
          <o:AllowPNG />
          <o:PixelsPerInch>96</o:PixelsPerInch>
        </o:OfficeDocumentSettings>
      </xml>
    <![endif]-->
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <meta name=""x-apple-disable-message-reformatting"" />
    <!--[if !mso]><!-->
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" />
    <!--<![endif]-->
    <title></title>

    <style type=""text/css"">
      @media only screen and (min-width: 520px) {{
        .u-row {{
          width: 500px !important;
        }}
        .u-row .u-col {{
          vertical-align: top;
        }}

        .u-row .u-col-100 {{
          width: 500px !important;
        }}
      }}

      @media (max-width: 520px) {{
        .u-row-container {{
          max-width: 100% !important;
          padding-left: 0px !important;
          padding-right: 0px !important;
        }}
        .u-row .u-col {{
          min-width: 320px !important;
          max-width: 100% !important;
          display: block !important;
        }}
        .u-row {{
          width: 100% !important;
        }}
        .u-col {{
          width: 100% !important;
        }}
        .u-col > div {{
          margin: 0 auto;
        }}
      }}
      body {{
        margin: 0;
        padding: 0;
      }}

      table,
      tr,
      td {{
        vertical-align: top;
        border-collapse: collapse;
      }}

      p {{
        margin: 0;
      }}

      .ie-container table,
      .mso-container table {{
        table-layout: fixed;
      }}

      * {{
        line-height: inherit;
      }}

      a[x-apple-data-detectors=""true""] {{
        color: inherit !important;
        text-decoration: none !important;
      }}

      table,
      td {{
        color: #000000;
      }}
      #u_body a {{
        color: #0000ee;
        text-decoration: underline;
      }}
    </style>
  </head>

  <body
    class=""clean-body u_body""
    style=""
      margin: 0;
      padding: 0;
      -webkit-text-size-adjust: 100%;
      background-color: #f7f8f9;
      color: #000000;
    ""
  >
    <!--[if IE]><div class=""ie-container""><![endif]-->
    <!--[if mso]><div class=""mso-container""><![endif]-->
    <table
      id=""u_body""
      style=""
        border-collapse: collapse;
        table-layout: fixed;
        border-spacing: 0;
        mso-table-lspace: 0pt;
        mso-table-rspace: 0pt;
        vertical-align: top;
        min-width: 320px;
        margin: 0 auto;
        background-color: #f7f8f9;
        width: 100%;
      ""
      cellpadding=""0""
      cellspacing=""0""
    >
      <tbody>
        <tr style=""vertical-align: top"">
          <td
            style=""
              word-break: break-word;
              border-collapse: collapse !important;
              vertical-align: top;
            ""
          >
            <!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td align=""center"" style=""background-color: #F7F8F9;""><![endif]-->

            <div
              class=""u-row-container""
              style=""padding: 0px; background-color: transparent""
            >
              <div
                class=""u-row""
                style=""
                  margin: 0 auto;
                  min-width: 320px;
                  max-width: 500px;
                  overflow-wrap: break-word;
                  word-wrap: break-word;
                  word-break: break-word;
                  background-color: transparent;
                ""
              >
                <div
                  style=""
                    border-collapse: collapse;
                    display: table;
                    width: 100%;
                    height: 100%;
                    background-color: transparent;
                  ""
                >
                  <!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding: 0px;background-color: transparent;"" align=""center""><table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""width:500px;""><tr style=""background-color: transparent;""><![endif]-->

                  <!--[if (mso)|(IE)]><td align=""center"" width=""500"" style=""background-color: #84abff;width: 500px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"" valign=""top""><![endif]-->
                  <div
                    class=""u-col u-col-100""
                    style=""
                      max-width: 320px;
                      min-width: 500px;
                      display: table-cell;
                      vertical-align: top;
                    ""
                  >
                    <div
                      style=""
                        background-color: #84abff;
                        height: 100%;
                        width: 100% !important;
                        border-radius: 0px;
                        -webkit-border-radius: 0px;
                        -moz-border-radius: 0px;
                      ""
                    >
                      <!--[if (!mso)&(!IE)]><!--><div
                        style=""
                          box-sizing: border-box;
                          height: 100%;
                          padding: 0px;
                          border-top: 0px solid transparent;
                          border-left: 0px solid transparent;
                          border-right: 0px solid transparent;
                          border-bottom: 0px solid transparent;
                          border-radius: 0px;
                          -webkit-border-radius: 0px;
                          -moz-border-radius: 0px;
                        ""
                      ><!--<![endif]-->
                        <table
                          style=""font-family: arial, helvetica, sans-serif""
                          role=""presentation""
                          cellpadding=""0""
                          cellspacing=""0""
                          width=""100%""
                          border=""0""
                        >
                          <tbody>
                            <tr>
                              <td
                                style=""
                                  overflow-wrap: break-word;
                                  word-break: break-word;
                                  padding: 10px;
                                  font-family: arial, helvetica, sans-serif;
                                ""
                                align=""left""
                              >
                                <table
                                  width=""100%""
                                  cellpadding=""0""
                                  cellspacing=""0""
                                  border=""0""
                                >
                                  <tr>
                                    <td
                                      style=""
                                        padding-right: 0px;
                                        padding-left: 0px;
                                      ""
                                      align=""center""
                                    >
                                      <a
                                        href=""https://ifbc.co/""
                                        target=""_blank""
                                      >
                                        <img
                                          align=""center""
                                          border=""0""
                                          src=""https://ifbc.co/images/logo/IFBC%203.png""
                                          alt=""""
                                          title=""""
                                          style=""
                                            outline: none;
                                            text-decoration: none;
                                            -ms-interpolation-mode: bicubic;
                                            clear: both;
                                            display: inline-block !important;
                                            border: none;
                                            height: auto;
                                            float: none;
                                            width: 60%;
                                            max-width: 288px;
                                          ""
                                          width=""288""
                                        />
                                      </a>
                                    </td>
                                  </tr>
                                </table>
                              </td>
                            </tr>
                          </tbody>
                        </table>

                        <table
                          style=""font-family: arial, helvetica, sans-serif""
                          role=""presentation""
                          cellpadding=""0""
                          cellspacing=""0""
                          width=""100%""
                          border=""0""
                        >
                          <tbody>
                            <tr>
                              <td
                                style=""
                                  overflow-wrap: break-word;
                                  word-break: break-word;
                                  padding: 10px;
                                  font-family: arial, helvetica, sans-serif;
                                ""
                                align=""left""
                              >
                                <!--[if mso]><table width=""100%""><tr><td><![endif]-->
                                <h1
                                  style=""
                                    margin: 0px;
                                    color: #ffffff;
                                    line-height: 140%;
                                    text-align: center;
                                    word-wrap: break-word;
                                    font-family: arial, helvetica, sans-serif;
                                    font-size: 18px;
                                    font-weight: 400;
                                  ""
                                >
                                  <span
                                    ><span
                                      ><span
                                        ><span
                                          ><span
                                            ><strong
                                              >New Franchise Request!</strong
                                            ></span
                                          ></span
                                        ></span
                                      ></span
                                    ></span
                                  >
                                </h1>
                                <!--[if mso]></td></tr></table><![endif]-->
                              </td>
                            </tr>
                          </tbody>
                        </table>

                        <!--[if (!mso)&(!IE)]><!-->
                      </div>
                      <!--<![endif]-->
                    </div>
                  </div>
                  <!--[if (mso)|(IE)]></td><![endif]-->
                  <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->
                </div>
              </div>
            </div>

            <div
              class=""u-row-container""
              style=""padding: 0px; background-color: transparent""
            >
              <div
                class=""u-row""
                style=""
                  margin: 0 auto;
                  min-width: 320px;
                  max-width: 500px;
                  overflow-wrap: break-word;
                  word-wrap: break-word;
                  word-break: break-word;
                  background-color: transparent;
                ""
              >
                <div
                  style=""
                    border-collapse: collapse;
                    display: table;
                    width: 100%;
                    height: 100%;
                    background-color: transparent;
                  ""
                >
                  <!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding: 0px;background-color: transparent;"" align=""center""><table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""width:500px;""><tr style=""background-color: transparent;""><![endif]-->

                  <!--[if (mso)|(IE)]><td align=""center"" width=""500"" style=""background-color: #ededed;width: 500px;padding: 11px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"" valign=""top""><![endif]-->
                  <div
                    class=""u-col u-col-100""
                    style=""
                      max-width: 320px;
                      min-width: 500px;
                      display: table-cell;
                      vertical-align: top;
                    ""
                  >
                    <div
                      style=""
                        background-color: #ededed;
                        height: 100%;
                        width: 100% !important;
                        border-radius: 0px;
                        -webkit-border-radius: 0px;
                        -moz-border-radius: 0px;
                      ""
                    >
                      <!--[if (!mso)&(!IE)]><!--><div
                        style=""
                          box-sizing: border-box;
                          height: 100%;
                          padding: 11px;
                          border-top: 0px solid transparent;
                          border-left: 0px solid transparent;
                          border-right: 0px solid transparent;
                          border-bottom: 0px solid transparent;
                          border-radius: 0px;
                          -webkit-border-radius: 0px;
                          -moz-border-radius: 0px;
                        ""
                      ><!--<![endif]-->
                       
                        {emailBody}

                        <!--[if (!mso)&(!IE)]><!-->
                      </div>
                      <!--<![endif]-->
                    </div>
                  </div>
                  <!--[if (mso)|(IE)]></td><![endif]-->
                  <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->
                </div>
              </div>
            </div>

            <div
              class=""u-row-container""
              style=""padding: 0px; background-color: transparent""
            >
              <div
                class=""u-row""
                style=""
                  margin: 0 auto;
                  min-width: 320px;
                  max-width: 500px;
                  overflow-wrap: break-word;
                  word-wrap: break-word;
                  word-break: break-word;
                  background-color: transparent;
                ""
              >
                <div
                  style=""
                    border-collapse: collapse;
                    display: table;
                    width: 100%;
                    height: 100%;
                    background-color: transparent;
                  ""
                >
                  <!--[if (mso)|(IE)]><table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""><tr><td style=""padding: 0px;background-color: transparent;"" align=""center""><table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""width:500px;""><tr style=""background-color: transparent;""><![endif]-->

                  <!--[if (mso)|(IE)]><td align=""center"" width=""500"" style=""background-color: #ededed;width: 500px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"" valign=""top""><![endif]-->
                  <div
                    class=""u-col u-col-100""
                    style=""
                      max-width: 320px;
                      min-width: 500px;
                      display: table-cell;
                      vertical-align: top;
                    ""
                  >
                    <div
                      style=""
                        background-color: #ededed;
                        height: 100%;
                        width: 100% !important;
                        border-radius: 0px;
                        -webkit-border-radius: 0px;
                        -moz-border-radius: 0px;
                      ""
                    >
                      <!--[if (!mso)&(!IE)]><!--><div
                        style=""
                          box-sizing: border-box;
                          height: 100%;
                          padding: 0px;
                          border-top: 0px solid transparent;
                          border-left: 0px solid transparent;
                          border-right: 0px solid transparent;
                          border-bottom: 0px solid transparent;
                          border-radius: 0px;
                          -webkit-border-radius: 0px;
                          -moz-border-radius: 0px;
                        ""
                      ><!--<![endif]-->
                        <table
                          style=""font-family: arial, helvetica, sans-serif""
                          role=""presentation""
                          cellpadding=""0""
                          cellspacing=""0""
                          width=""100%""
                          border=""0""
                        >
                          <tbody>
                            <tr>
                              <td
                                style=""
                                  overflow-wrap: break-word;
                                  word-break: break-word;
                                  padding: 10px;
                                  font-family: arial, helvetica, sans-serif;
                                ""
                                align=""left""
                              >
                                <div class=""menu"" style=""text-align: center"">
                                  <!--[if (mso)|(IE)]><table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center""><tr><![endif]-->

                                  <!--[if (mso)|(IE)]><td style=""padding:5px 15px""><![endif]-->

                                  <a
                                    href=""https://ifbc.co/privacy-policy""
                                    target=""_blank""
                                    style=""
                                      padding: 5px 15px;
                                      display: inline-block;
                                      color: #4d4d4d;
                                      font-family: arial, helvetica, sans-serif;
                                      font-size: 12px;
                                      text-decoration: none;
                                    ""
                                  >
                                    Privacy policy
                                  </a>

                                  <!--[if (mso)|(IE)]></td><![endif]-->

                                  <!--[if (mso)|(IE)]><td style=""padding:5px 15px""><![endif]-->

                                  <a
                                    href=""https://ifbc.co/terms-conditions""
                                    target=""_self""
                                    style=""
                                      padding: 5px 15px;
                                      display: inline-block;
                                      color: #4d4d4d;
                                      font-family: arial, helvetica, sans-serif;
                                      font-size: 12px;
                                      text-decoration: none;
                                    ""
                                  >
                                    Terms & Conditions
                                  </a>

                                  <!--[if (mso)|(IE)]></td><![endif]-->

                                  <!--[if (mso)|(IE)]></tr></table><![endif]-->
                                </div>
                              </td>
                            </tr>
                          </tbody>
                        </table>

                        <table
                          style=""font-family: arial, helvetica, sans-serif""
                          role=""presentation""
                          cellpadding=""0""
                          cellspacing=""0""
                          width=""100%""
                          border=""0""
                        >
                          <tbody>
                            <tr>
                              <td
                                style=""
                                  overflow-wrap: break-word;
                                  word-break: break-word;
                                  padding: 10px;
                                  font-family: arial, helvetica, sans-serif;
                                ""
                                align=""left""
                              >
                                <div
                                  style=""
                                    font-family: arial, helvetica, sans-serif;
                                    font-size: 10px;
                                    line-height: 140%;
                                    text-align: center;
                                    word-wrap: break-word;
                                  ""
                                >
                                  <p style=""line-height: 140%"">
                                    2024 - Powered by International Franchise
                                    Business Consultant Corp. 
                                  </p>
                                </div>
                              </td>
                            </tr>
                          </tbody>
                        </table>

                        <!--[if (!mso)&(!IE)]><!-->
                      </div>
                      <!--<![endif]-->
                    </div>
                  </div>
                  <!--[if (mso)|(IE)]></td><![endif]-->
                  <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->
                </div>
              </div>
            </div>

            <!--[if (mso)|(IE)]></td></tr></table><![endif]-->
          </td>
        </tr>
      </tbody>
    </table>
    <!--[if mso]></div><![endif]-->
    <!--[if IE]></div><![endif]-->
  </body>
</html>

            ";
            SendEmail(body, "sm@ifbc.co", "IFBC");

            return CreatedAtAction(nameof(GetCheckout), new { id = checkout.DocId }, checkout);
        }
        catch (Exception ex)
        {
            // Log the exception (use your preferred logging mechanism)
            Console.WriteLine($"An error occurred: {ex.Message}");
            return StatusCode(500, new { Message = "Internal server error", Detail = ex.Message });
        }
    }

    // GET: api/YourTableName
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Checkout>>> GetCheckout()
    {
        return await _context.Checkout.ToListAsync();
    }
}
