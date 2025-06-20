namespace Revenda.Application.Util
{
    public static class Tools
    {
        public static bool CNPJIsValid(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            // Remove caracteres não numéricos
            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Replace(" ", "");

            // Verifica se tem 14 dígitos
            if (cnpj.Length != 14)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cnpj.All(c => c == cnpj[0]))
                return false;

            // Valida primeiro dígito verificador
            int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;
            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(cnpj[i].ToString()) * multiplicadores1[i];
            }
            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            if (int.Parse(cnpj[12].ToString()) != digitoVerificador1)
                return false;

            // Valida segundo dígito verificador
            int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(cnpj[i].ToString()) * multiplicadores2[i];
            }
            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            return int.Parse(cnpj[13].ToString()) == digitoVerificador2;
        }

        public static bool CPFIsValid(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            // Remove caracteres não numéricos
            cpf = cpf.Replace(".", "").Replace("-", "").Replace(" ", "");

            // Verifica se tem 11 dígitos
            if (cpf.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cpf.All(c => c == cpf[0]))
                return false;

            // Valida primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            }
            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            if (int.Parse(cpf[9].ToString()) != digitoVerificador1)
                return false;

            // Valida segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            }
            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            return int.Parse(cpf[10].ToString()) == digitoVerificador2;
        }
    }
}
