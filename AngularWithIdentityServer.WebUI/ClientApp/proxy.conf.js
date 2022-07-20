const { env } = require('process');

// There is not any controller for root api
// Uncomment this if you will use root controllers (e.g. for configuration)

// const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
//   env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:3395';

const apiRoot = env.FOREIGN_API_PORT ? `https://localhost:${env.FOREIGN_API_PORT}` :
  env.FOREIGN_API_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:5233';

const identityServer = env.IDENTITY_SERVER_PORT ? `https://localhost:${env.IDENTITY_SERVER_PORT}` :
  env.IDENTITY_SERVER_URLS ? env.IDENTITY_SERVER_URLS.split(';')[0] : 'http://localhost:5006';

const PROXY_CONFIGS = [
  // {
  //   context: [
  //     "/weatherforecast",
  //  ],
  //   target: target,
  //   secure: false,
  //   headers: {
  //     Connection: 'Keep-Alive'
  //   }
  // },
  {
    context: [
      "/api"
    ],
    target: apiRoot,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  },
  {
    context: [],
    target: identityServer,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIGS;
