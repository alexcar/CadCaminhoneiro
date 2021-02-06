import { MarcaCaminhao } from './marcaCaminhao';

export interface MarcaCaminhaoResult {
  isError: boolean;
  message: string;
  statusCode: number;
  version: string;
  result: {
    pageNumber: number;
    pageSize: number;
    firstPage: string;
    lastPage: string;
    totalPages: number;
    totalRecords: number;
    nextPage: string;
    data: MarcaCaminhao[]
  }
}
