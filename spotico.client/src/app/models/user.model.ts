import { v4 as uuid } from "uuid";

export class User {
  id: string;
  username: string;
  email: string;
  password: string;
  role: string;

  constructor() {
    this.id = this.id || uuid();
  }
}