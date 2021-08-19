using System;

namespace DesafioGlobaltec.Domain.Models {
    public class Pessoa {
        private string _CodPessoa;
        public string CodigoPessoa {
            get => _CodPessoa;
            set => _CodPessoa = value?.Trim();
        }

        private string _nomePessoa;
        public string NomePessoa {
            get => _nomePessoa;
            set => _nomePessoa = value?.Trim().ToUpper();
        }

        private string _cpfPessoas;
        public string CPFPessoas {
            get => _cpfPessoas;
            set => _cpfPessoas = value?.Trim().ToUpper();
        }

        private string _ufPessoa;
        public string UFPessoa {
            get => _ufPessoa;
            set => _ufPessoa = value?.Trim().ToUpper();
        }

        private DateTime _dtNascimentoPessoa;
        public string DtNascimentoPessoa {
            get => _dtNascimentoPessoa.ToString("dd/MM/yyyy");
            set => _dtNascimentoPessoa = DateTime.ParseExact(value.Trim().ToString(), "dd/MM/yyyy", null);
        }
    }
}