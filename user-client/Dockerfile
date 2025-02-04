FROM node:lts-alpine as build-stage
ARG VITE_VUE_APP_ENV
ENV VITE_VUE_APP_ENV $VITE_VUE_APP_ENV
WORKDIR /app
COPY client/package*.json ./
RUN npm install
COPY client/. .
RUN npm run build

FROM nginx:stable-alpine as production-stage

COPY --from=build-stage /app/dist /usr/share/nginx/html
COPY client/nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
