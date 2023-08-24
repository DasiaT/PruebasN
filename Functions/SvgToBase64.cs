using NinjaTalentCountrys.Models;
using System.Text;

namespace NinjaTalentCountrys.Functions
{
    public class SvgToBase64
    {

        public async Task<string> ConvertSvgUrlToBase64Async(string svgUrl)
        {
            using (HttpClient client = new())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(svgUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string svgText = await response.Content.ReadAsStringAsync();
                        byte[] svgBytes = Encoding.UTF8.GetBytes(svgText);
                        string base64String = Convert.ToBase64String(svgBytes);
                        return base64String;
                    }
                    else
                    {
                        throw new Exception("Error fetching SVG from URL. Status code: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred: " + ex.Message);
                }
            }
        }

        public int validacion(string dato)
        {
            if (!string.IsNullOrEmpty(dato))
            {
                return 1;
            }

            return 0;
        }
       
    }
}
