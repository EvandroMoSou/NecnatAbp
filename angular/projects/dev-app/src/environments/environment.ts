import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'NecnatAbp',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44347/',
    redirectUri: baseUrl,
    clientId: 'NecnatAbp_App',
    responseType: 'code',
    scope: 'offline_access NecnatAbp',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44347',
      rootNamespace: 'NecnatAbp',
    },
    NecnatAbp: {
      url: 'https://localhost:44355',
      rootNamespace: 'NecnatAbp',
    },
  },
} as Environment;
