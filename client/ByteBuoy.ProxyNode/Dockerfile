FROM node:14

# Create app directory
WORKDIR /usr/src/app

# Install app dependencies
COPY .env .env
COPY package*.json ./
RUN npm install

# Bundle app source
COPY . .

EXPOSE 8080
CMD ["node", "proxy.js"]
