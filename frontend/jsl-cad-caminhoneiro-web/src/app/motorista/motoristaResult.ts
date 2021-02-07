import { Motorista } from "./motorista";

export interface MotoristaResult {
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
    data: Motorista[]
  }
}
