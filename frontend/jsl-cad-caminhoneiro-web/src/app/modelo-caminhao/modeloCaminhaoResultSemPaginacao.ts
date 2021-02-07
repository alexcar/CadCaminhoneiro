import { ModeloCaminhao } from "./modeloCaminhao";

export interface ModeloCaminhaoResultSemPaginacao {
  version: string;
  statusCode: number;
  message: string;
  isError: boolean;
  result: ModeloCaminhao[];
}
