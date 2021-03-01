using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RockPaperScissors.Core;
using System.Text;

namespace RockPaperScissors.TagHelpers
{
	[HtmlTargetElement("alert", TagStructure = TagStructure.WithoutEndTag)]
	[HtmlTargetElement("alert-tempdata", TagStructure = TagStructure.WithoutEndTag)]
	public class BootstrapAlertTagHelper : TagHelper
	{

		private const string AlertTypeAttributeName = "alert-type";

		[HtmlAttributeName(AlertTypeAttributeName)]
		public BootstrapAlertType AlertType { get; set; } = BootstrapAlertType.UNKNOWN;

		public string Message { get; set; }

		public string Title { get; set; }

		public bool CloseButton { get; set; } = false;

		public bool HeaderSeparator { get; set; } = false;

		public bool Visible { get; set; } = false;

		[ViewContext]
		public ViewContext ViewContext { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			bool fromTempData = context.TagName == "alert-tempdata";
			if (fromTempData)
				this.ReadFromTempData();

			if (this.Visible)
			{
				string cssCloseButton = this.CloseButton ? "alert-dismissible fade show" : string.Empty;
				string cssClass = $"alert {Bootstrap.ConvertAlertTypeToString(this.AlertType)} {cssCloseButton}";
				output.TagName = "div";
				output.TagMode = TagMode.StartTagAndEndTag;
				output.Attributes.Add("class", cssClass);
				output.Attributes.Add("role", "alert");
				output.Content.SetHtmlContent(this.GetHtmlContent());
			}
			else
				output.SuppressOutput();

			base.Process(context, output);
		}

		private void ReadFromTempData()
		{
			ITempDataDictionary temp = this.ViewContext?.TempData;
			if (temp.ContainsKey("AlertVisible") && (bool)temp["AlertVisible"])
			{
				this.Title = (string)temp["AlertTitle"];
				this.Message = (string)temp["AlertMessage"];
				if (temp["AlertType"] is int value)
					this.AlertType = (BootstrapAlertType)value;
				if (temp["AlertType"] is BootstrapAlertType type)
					this.AlertType = type;
				this.CloseButton = (bool)temp["CloseButton"];
				this.HeaderSeparator = (bool)temp["HeaderSeparator"];
				this.Visible = (bool)temp["AlertVisible"];
			}
		}

		private string GetHtmlContent()
		{
			StringBuilder sb = new StringBuilder();

			//Close button
			if (this.CloseButton)
				sb.Append("<button type =\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">\r\n" +
					"<span aria-hidden=\"true\">&times;</span>\r\n" +
					"</button>\r\n");

			//Title (Header) and message
			sb.Append("<div class=\"alert-text\">");
			if (!string.IsNullOrEmpty(this.Title))
				sb.Append($"<h4>{this.Title}</h4>" + (this.HeaderSeparator ? "<hr/>" : string.Empty));
			sb.Append($"<p>{this.Message}</p>");
			sb.Append("</div>");

			return sb.ToString();
		}

	}
}
