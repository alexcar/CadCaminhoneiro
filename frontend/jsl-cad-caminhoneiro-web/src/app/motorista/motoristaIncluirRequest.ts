export interface MotoristaIncluirRequest {
  nome: string;
  cpf: string;
  dataNascimento: Date;
  nomePai: string;
  nomeMae: string;
  naturalidade: string;
  numeroRegistroGeral: string;
  orgaoExpedicaoRegistroGeral: string;
  dataExpedicaoRegistroGeral: Date;
  logradouro: string;
  numero: string;
  complemento: string;
  bairro: string;
  municipio: string;
  uf: string;
  cep: string;
  numeroRegistroHabilitacao: string;
  categoriaHabilitacao: string;
  dataPrimeiraHabilitacao: Date;
  dataValidadeHabilitacao: Date;
  dataEmissaoHabilitacao: Date;
  observacaoHabilitacao: string;
  placa: string;
  eixo: string;
  caminhaoObservacao: string;
  marcaCaminhaoId: string;
  modeloCaminhaoId: string;
}