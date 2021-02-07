import { Motorista } from "./motorista";

export interface MotoristaResultSemPaginacao {
  version: string;
  statusCode: number;
  message: string;
  isError: boolean;
  result: Motorista[];
}
