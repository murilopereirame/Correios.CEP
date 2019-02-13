// Correios.CEP - API destinada a .NET com objetivo de realizar
//a busca de endereços a partir do CEP através do site dos Correios.
// Copyright (C) 2019 Murilo Rocha Peraira <mrex@tuta.io>
// Copyright (C) 2019 Pereira Tech <mrex@tuta.io>

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Correios.CEP
{
    public class correiosCEP
    {
        public static cepConsulta GetAddress(string cep)
        {
            var end = new cepConsulta();
            end.Cep = cep;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.buscacep.correios.com.br/sistemas/buscacep/resultadoBuscaCepEndereco.cfm");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] postBytes = Encoding.ASCII.GetBytes("relaxation=" + cep.Replace("-", "") + "&tipoCEP=ALL&semelhante=N");

            request.GetRequestStream()
                .Write(postBytes, 0, postBytes.Length);

            string responseText = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding("ISO-8859-1")).ReadToEnd();

            string s = responseText.Substring(responseText.LastIndexOf("<tr>"));
            s = s.Substring(0, s.LastIndexOf("</tr>"));
            s = s.Replace("<tr>", "");
            s = s.Replace("</tr>", "");
            s = s.Replace("&nbsp;", "");
            s = s.Replace("\r", "\n");

            List<string> Address = new List<string>();
            const string pattern = @"<td\b[^>]*?>(?<V>[\s\S]*?)</\s*td>";
            foreach (Match match in Regex.Matches(s, pattern, RegexOptions.IgnoreCase))
            {
                string value = match.Groups["V"].Value;
                Address.Add(value);
            }

            end.Rua = Address[0];
            end.Bairro = Address[1];
            end.Cidade = Address[2].Substring(0, Address[2].LastIndexOf("/"));
            end.UF = Address[2].Substring(Address[2].LastIndexOf("/") + 1);

            if (Address[0] == "")
                end.universalCep = true;

            return end;
        }
    }
}
