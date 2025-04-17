# Build stage
FROM node:18-alpine AS build
WORKDIR /app

# Install git and dependencies
RUN apk add --no-cache git

# Copy package.json first
COPY frontend/package.json ./

# Install dependencies
RUN npm install --legacy-peer-deps

# Copy the rest of the code
COPY frontend/ .

# Install Angular CLI globally
RUN npm install -g @angular/cli@18.1.2

# Create styles.css if it doesn't exist
#RUN touch src/styles.css

# Build the application based on environment
ARG BUILD_ENV=development
RUN echo "Building for environment: $BUILD_ENV" && \
    if [ "$BUILD_ENV" = "production" ]; then \
        ng build --configuration production; \
    elif [ "$BUILD_ENV" = "uat" ]; then \
        ng build --configuration uat; \
    elif [ "$BUILD_ENV" = "staging" ]; then \
        ng build --configuration staging; \
    else \
        ng build --configuration development; \
    fi

# Production stage with Nginx
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Remove default nginx static assets
RUN rm -rf ./*

# Copy static assets from builder stage
COPY --from=build /app/dist/SuperQA/ .

# Copy nginx configuration
COPY docker/frontend/nginx.conf /etc/nginx/conf.d/default.conf

# Add bash for debugging if needed
RUN apk add --no-cache bash

# Create directory for logs
RUN mkdir -p /var/log/nginx

# Expose ports
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]
