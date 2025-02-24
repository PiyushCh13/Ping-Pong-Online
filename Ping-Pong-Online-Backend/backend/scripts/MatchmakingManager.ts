import { Socket } from "socket.io";
import { User_Plain } from "../src/common/schemas-to-ts/User";

export type QueuePlayer = {
    id: number,
    username: string,
    socket: Socket;
};

export type InMatchPlayer = {
    id: number,
    username: string,
    socket: Socket;
};

class Match {
  player1: InMatchPlayer;
  player2: InMatchPlayer;

  constructor(player1: InMatchPlayer, player2: InMatchPlayer) {
    this.player1 = player1;
    this.player2 = player2;
  }

  Start() {
    const matchData = {
        player1: {
            id: this.player1.id,
            username: this.player1.username
        },
        player2: {
            id: this.player2.id,
            username: this.player2.username
        }
    };

    this.player1.socket.emit('matchFound', matchData);
    this.player2.socket.emit('matchFound', matchData);
}
}

let queue: QueuePlayer[] = [];

export const addtoQueue = async (player: QueuePlayer) => {
    if (queue.length > 0) {
        const opponent = queue.shift();
        if (!opponent) {
            return;
        }

        const player1fromStrapi: User_Plain = await strapi.db
            .query("plugin::users-permissions.user")
            .findOne({ where: { id: player.id } });
        
        const player2fromStrapi: User_Plain = await strapi.db
            .query("plugin::users-permissions.user")
            .findOne({ where: { id: opponent.id } });

        const player1: InMatchPlayer = {
            id: player.id,
            username: player1fromStrapi.username,
            socket: player.socket,
        };

        const player2: InMatchPlayer = {
            id: opponent.id,
            username: player2fromStrapi.username,
            socket: opponent.socket,
        };

        const match = new Match(player1, player2);
        match.Start();

        

    } else {
        queue.push(player);
    }
};
