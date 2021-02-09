import { MarcaCaminhao } from "../marca-caminhao/marcaCaminhao";
import { ModeloCaminhao } from "../modelo-caminhao/modeloCaminhao";

export interface Motorista {
  id: string;
  nome: string;
  cpf: string;
  dataNascimento: string;
  nomePai: string;
  nomeMae: string;
  naturalidade: string;
  numeroRegistroGeral: string;
  orgaoExpedicaoRegistroGeral: string;
  dataExpedicaoRegistroGeral: string;
  enderecoDto: EnderecoDto,
  habilitacaoDto: HabilitacaoDto,
  caminhaoDto: CaminhaoDto
}

export interface EnderecoDto {
  id: string;
  motoristaId: string;
  logradouro: string;
  numero: string;
  complemento: string;
  bairro: string;
  municipio: string;
  uf: string;
  cep: string;
}

export interface HabilitacaoDto {
  id: string;
  motoristaId: string;
  numeroRegistro: string;
  categoria: string;
  dataPrimeiraHabilitacao: string;
  dataValidade: string;
  dataEmissao: string;
  observacao: string;
}

export interface CaminhaoDto {
  id: string;
  motoristaId: string;
  placa: string;
  eixo: string;
  observacao: string;
  marcaCaminhaoListDto: MarcaCaminhao;
  modeloCaminhaoListDto: ModeloCaminhao;
}
