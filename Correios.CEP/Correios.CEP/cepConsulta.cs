// Correios.CEP - API destinada a .NET com objetivo de realizar 
// a busca de endereços a partir do CEP através do site dos Correios.
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

namespace Correios.CEP
{
    public class cepConsulta
    {
        private string _Cep;
        private string _Rua;
        private string _Cidade;
        private string _Bairro;
        private string _Estado;

        /// <summary>
        /// A validação para o CEP permite apenas strings de
        /// oito nove digitos com ou sem máscara, apenas seguindo
        /// os seguintes padroes: 00000999 ou 00000-999.
        /// </summary>
        /// 

        public string Cep
        {
            get
            {
                return this._Cep;
            }
            set
            {
                int dCount = 0;

                if (value.Length != 8 && value.Length != 9)
                    throw new ArgumentException("O CEP informado é inválido!\nO CEP deve conter 8 dígitos (sem hífen) ou 9 (com hífen)");

                foreach (char c in value)
                    if (Char.IsDigit(c))
                        dCount++;

                if (dCount != 8)
                    throw new ArgumentException("O CEP informado é inválido!");

                this._Cep = value;
            }
        }

        /// <summary>
        /// A validação para o nome da Rua permite apenas strings
        /// respeitando o limite máximo de 500 caracteres.        
        /// </summary>
        /// 
        public string Rua
        {
            get
            {
                return this._Rua;
            }
            set
            {
                if (value.Length > 500)
                    throw new ArgumentException("O nome da rua não pode exceder 500 caracteres!");

                this._Rua = value;
            }
        }

        /// <summary>
        /// A validação para o nome da cidade permite apenas strings
        /// respeitando o limite máximo de 30 caracteres.
        /// De acordo com o Google,
        /// a cidade com maior nome do Brasil possui 28 caracteres.
        /// </summary>
        /// 
        public string Cidade
        {
            get
            {
                return this._Cidade;
            }
            set
            {
                if (value.Length > 30)
                    throw new ArgumentException("O nome da cidade não pode exceder 30 caracteres!");
                this._Cidade = value;
            }
        }

        /// <summary>
        /// A validação para o Bairro permite apenas strings
        /// respeitando o limite máximo de 500 caracteres.        
        /// </summary>
        /// 
        public string Bairro
        {
            get
            {
                return this._Bairro;
            }
            set
            {
                if (value.Length > 500)
                    throw new ArgumentException("O nome da cidade não pode exceder 500 caracteres!");
                this._Bairro = value;
            }
        }

        /// <summary>
        /// A validação para o estado permite apenas strings
        /// respeitando o limite de 2 caracteres.        
        /// A validação é feita através de um array de strings
        /// que contem todas as siglas dos estados pertencentes
        /// ao Brasil.
        /// </summary>
        /// 
        public string UF
        {
            get
            {
                return this._Estado;
            }
            set
            {
                bool ok = false;
                string[] estados =
                {
                    "AC","AL","AM","AP","BA","CE","DF","ES","GO","MA","MG","MS","MT","PA","PB","PE","PI","PR",
                    "RJ","RN","RO","RR","RS","SC","SE","SP","TO"
                };

                foreach (string s in estados)
                {
                    if (s.ToUpper() == value)
                    {
                        ok = true;
                        this._Estado = value;
                    }
                }

                if (!ok)
                    throw new ArgumentException("O estado informado não é um estado válido do Brasil!");
            }
        }

        /// <summary>
        /// Caso a cidade possua um CEP único para
        /// todos os endereços, universalCep se torna verdadeiro.
        /// </summary>
        /// 
        public bool universalCep { get; set; }
    }
}
