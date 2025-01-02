import { v4 as uuid } from "uuid";
import { Playlist } from "./playlist.model";

export class User {
  id: string;
  username: string;
  email: string;
  bio: string;
  password: string;
  role: string;

  constructor() {
    this.id = this.id || uuid();
  }
}