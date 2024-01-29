

# Deployment Using Traefik as an Edge Router (Docker Swarm)

This section outlines a deployment strategy for ByteBuoy utilizing Traefik.io, a powerful edge router, to establish a comprehensive ByteBuoy environment. This environment includes essential components such as the Frontend, API-Backend, and Proxy.

The strategy leverages the current Git repository as a base, employing it to build the necessary Docker images for deployment. This approach ensures that the most up-to-date codebase is used in the deployment process. 

1) Clone the git repository into a sub folder

`git clone https://github.com/WeLytics/ByteBuoy.git`


1) Copy `build-docker-compose.yml`, `traefik-docker-compose.yml` to the current directory

## Linux
```
cp ./ByteBuoy/scripts/deployment/traefik-docker-swarm/build-docker-compose.yml .
cp ./ByteBuoy/scripts/deployment/traefik-docker-swarm/traefik-docker-compose.yml .
```


## Windows
```
copy .\ByteBuoy\scripts\deployment\traefik-docker-swarm\build-docker-compose.yml .
copy .\ByteBuoy\scripts\deployment\traefik-docker-swarm\traefik-docker-compose.yml .
```

1) Modify the traefik-docker-compose.yml file to include your specific Traefik labels, such as Certresolver and Hosts. Additionally, ensure that the environment variables are correctly set. Refer to the comments within the file for detailed guidance and examples.

1) Modify `/client/ByteBuoy.Web/.env.docker`

Set your defined proxy domain name to the environment variable `VITE_BACKEND_API`. Make sure the url path `/proxy` remains in place

```yaml
VITE_BACKEND_API_URI=https://bytebuoyproxy.yourdomain.com/proxy
```

1) Modify `/src/ByteBuoy.API/appsettings.json.docker` and set CORS Origins.

Make sure, you also set the Proxy Domain Name as a CORS origin for the API Project within the `appsettings.json.docker`.

```json
        "Cors": {
               "Origins": "https://bytebuoyproxy.yourdomain.com"
        },

```


1) Build the Docker Images

`docker-compose -f build-docker-compose.yml  build`

1) Start and Deploy Images to Traefik

`docker stack deploy --compose-file traefik-docker-compose.yml traefik`