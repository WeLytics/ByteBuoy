FROM nginx:latest

ENV API_KEY=XXXXX

COPY nginx.conf /etc/nginx/nginx.conf
#conf.d/default.conf

COPY default.conf.template /etc/nginx/templates/default.conf.template
#CMD /bin/bash -c "envsubst '$${API_KEY}' < /etc/nginx/templates/nginx.conf.template > /etc/nginx/nginx.conf && nginx -g 'daemon off;'"
# Command to run when the container starts
CMD /bin/bash -c "envsubst '$${API_KEY}' < /etc/nginx/templates/default.conf.template > /etc/nginx/nginx.conf && nginx -g 'daemon off;'"


# CMD nginx -g 'daemon off;'