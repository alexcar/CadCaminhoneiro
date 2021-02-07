import { MarcaCaminhao } from "../marca-caminhao/marcaCaminhao";
import { ModeloCaminhao } from "../modelo-caminhao/modeloCaminhao";

export interface Motorista {
  id: string;
  nome: string;
  cpf: string;
  dataNascimento: Date;
  nomePai: string;
  nomeMae: string;
  naturalidade: string;
  numeroRegistroGeral: string;
  orgaoExpedicaoRegistroGeral: string;
  dataExpedicaoRegistroGeral: Date;
  EnderecoDto: EnderecoDto,
  HabilitacaoDto: HabilitacaoDto,
  CaminhaoDto: CaminhaoDto
}

export interface EnderecoDto {
  Id: string;
  MotoristaId: string;
  Logradouro: string;
  Numero: string;
  Complemento: string;
  Bairro: string;
  Municipio: string;
  Uf: string;
  Cep: string;
}

export interface HabilitacaoDto {
  Id: string;
  MotoristaId: string;
  NumeroRegistro: string;
  Categoria: string;
  DataPrimeiraHabilitacao: Date;
  DataValidade: Date;
  DataEmissao: Date;
  Observacao: string;
}

export interface CaminhaoDto {
  Id: string;
  MotoristaId: string;
  Placa: string;
  Eixo: string;
  Observacao: string;
  MarcaCaminhaoListDto: MarcaCaminhao;
  ModeloCaminhaoListDto: ModeloCaminhao;
}
