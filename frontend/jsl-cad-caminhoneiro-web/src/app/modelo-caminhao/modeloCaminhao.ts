export interface ModeloCaminhao {
  id: string;
  descricao: string;
  ano: string;
  marcaCaminhaoId: string;
  marcaCaminhaoListDto: MarcaCaminhaoListDto
}

interface MarcaCaminhaoListDto {
  id: string;
  descricao: string;
}
