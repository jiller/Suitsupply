import {Shortening} from "./shortening";

export interface Alteration {
  customerId: number;
  shortenSleeves: Shortening;
  shortenTrousers: Shortening;
  creationDate: Date;
  orderState: string;
}
