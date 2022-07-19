const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:3395';

const foreign = env.FOREIGN_API_PORT ? `https://localhost:${env.FOREIGN_API_PORT}` :
  env.FOREIGN_API_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:5233';

const PROXY_CONFIGS = [
  {
    context: [
      "/weatherforecast",
   ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  },
  {
    context: [
      "/api"
    ],
    target: foreign,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIGS;
