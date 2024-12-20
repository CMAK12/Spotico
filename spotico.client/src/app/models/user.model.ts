import { UUID } from "crypto";

export class User {
  id: UUID;
  username: string;
  email: string;
  password: string;
  role: string;
}