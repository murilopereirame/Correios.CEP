# Correios.CEP
API destinada a .NET com objetivo de realizar a busca de endereços a partir do CEP através do site dos Correios

## Download da DLL
[GitHub - Direct](https://github.com/murilopereirame/Correios.CEP/raw/master/Correios.CEP/Correios.CEP/bin/Debug/netstandard2.0/Correios.CEP.dll)

## Exemplo:

```
cepConsulta address = correiosCEP.GetAddress(19010-270);
Console.WriteLine(address.Rua);
Console.WriteLine(address.Bairro);
Console.WriteLine(address.Cidade);
Console.WriteLine(address.UF);
Console.WriteLine(address.Cep);
```

## Output:
```
Avenida Manoel Goulart - até 529/530
Vila Nova
Presidente Prudente
19010-270
```

## Licença GNU 3.0
Arquivo [LICENSE](https://github.com/murilopereirame/Correios.CEP/blob/master/LICENSE)
