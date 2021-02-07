import { MarcaCaminhao } from './marcaCaminhao';

export interface MarcaCaminhaoResultSemPaginacao {
  version: string;
  statusCode: number;
  message: string;
  isError: boolean;
  result: MarcaCaminhao[]
}
