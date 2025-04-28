# Stage 1: Build
FROM node:20 AS build
WORKDIR /app

# Copy package files first for caching
COPY package.json ./

# Install dependencies and generate package-lock.json
RUN npm install

COPY angular.json ./
COPY tsconfig*.json ./
COPY src/ ./src/

# Build the application with default configuration
RUN npm run ng build

# Stage 2: Serve with Nginx
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Remove default nginx page
RUN rm -rf ./*

# Copy only the built files from build stage
COPY --from=build /app/dist/ /usr/share/nginx/html/

# Copy nginx configuration files
COPY nginx.conf /etc/nginx/nginx.conf
COPY default.conf /etc/nginx/conf.d/default.conf

# Create required directories and set permissions
RUN mkdir -p /var/log/nginx \
    && touch /var/log/nginx/error.log \
    && touch /var/log/nginx/access.log \
    && chown -R nginx:nginx /var/log/nginx

EXPOSE 8080
CMD ["nginx", "-g", "daemon off;"]
