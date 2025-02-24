import { Server } from "socket.io";
import socketio from "socket.io";
import jwt from "jsonwebtoken";
import dotenv from "dotenv";
import { addtoQueue, QueuePlayer } from "../scripts/MatchmakingManager";

export default {
  register() {},

  bootstrap({ strapi }) {
    const io = new socketio.Server(strapi.server.httpServer);

    io.on("connection", (socket) => {
      console.log("A user connected:", socket.id);

      socket.on("searchPlayer", (data) => {
        const myJwt = data.jwt;
        const JWTSecret = process.env.JWT_SECRET;

        try {
          const decoded = jwt.verify(myJwt, JWTSecret) as jwt.JwtPayload;

          const playerObjectforQueue: QueuePlayer = {
            id: decoded.id,
            username: decoded.username,
            socket: socket,
          };

          addtoQueue(playerObjectforQueue);
        } catch (err) {
          console.log("JWT Verification failed:", err);
        }
      });

      // Handle player movement
      socket.on("playerMove", (data) => {
        const { username, position } = data;

        // Broadcast movement to all other players
        socket.broadcast.emit("updatePlayerPosition", {
          username: username,
          position: position,
        });
      });

      // Handle player disconnection
      socket.on("disconnect", () => {
        console.log("A user disconnected:", socket.id);
      });
    });
  },
};
