import { User } from '../models/user.model';

export type PlayerTrack = {
  id: string;
  title: string;
  trackPath: string;
  artist: User;
  albumCoverUrl: string;
};
