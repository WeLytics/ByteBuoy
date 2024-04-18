using ByteBuoy.Domain.Entities;

namespace ByteBuoy.API.Utilities
{
	public class PageMetricBadgeGenerator
	{
		public static string GenerateBadge(Page page)
		{
			var color = page.PageStatus switch
			{
				Domain.Enums.MetricStatus.Success => "#4c1",
				Domain.Enums.MetricStatus.Warning => "orange",
				Domain.Enums.MetricStatus.Error => "red",
				Domain.Enums.MetricStatus.NoData => "gray",
				_ => throw new InvalidDataException("Invalid metric status"),
			};
			return GenerateBadge(page.Title, page.PageStatus.ToString(), "#555", color);
		}

		public static string GenerateBadge(string label, string message, string labelColor, string messageColor)
		{
			string svgTemplate = "<svg xmlns='http://www.w3.org/2000/svg' width='{0}' height='20'>" +
								 "<linearGradient id='b' x2='0' y2='100%'>" +
								 "<stop offset='0' stop-color='#bbb' stop-opacity='.1'/><stop offset='1' stop-opacity='.1'/></linearGradient>" +
								 "<mask id='a'><rect width='{0}' height='20' rx='3' fill='#fff'/></mask>" +
								 "<g mask='url(#a)'><path fill='{1}' d='M0 0h{2}v20H0z'/><path fill='{3}' d='M{2} 0h{4}v20H{2}z'/><path fill='url(#b)' d='M0 0h{0}v20H0z'/></g>" +
								 "<g fill='#fff' text-anchor='middle' font-family='Verdana,Geneva,sans-serif' font-size='11'>" +
								 "<text x='{5}' y='15'>{6}</text><text x='{7}' y='15'>{8}</text></g></svg>";

			// Calculate widths based on label and message lengths
			int labelWidth = 6 * label.Length + 10;  
			int messageWidth = 6 * message.Length + 10;  
			int totalWidth = labelWidth + messageWidth;

			// Calculate text positions
			int labelPositionX = labelWidth / 2;
			int messagePositionX = labelWidth + messageWidth / 2;

			// Populate the SVG template
			return string.Format(svgTemplate, totalWidth, labelColor, labelWidth, messageColor,
								 messageWidth, labelPositionX, label, messagePositionX, message);
		}

	}
}
