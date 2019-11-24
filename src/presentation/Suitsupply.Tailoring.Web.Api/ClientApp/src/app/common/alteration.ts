import {Shortening} from "./shortening";

export interface Alteration {
  id: number;
  customerId: number;
  shortenSleeves: Shortening;
  shortenTrousers: Shortening;
  creationDate: Date;
  payDate: Date;
  completeDate: Date;
  state: string;
}
