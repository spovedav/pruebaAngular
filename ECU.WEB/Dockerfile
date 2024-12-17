# Utilizar Node.js para construir la aplicación
FROM node:20 AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build --prod

# Utilizar Nginx para servir la aplicación Angular
FROM nginx:alpine
COPY --from=build /app/dist/ecu.web /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
