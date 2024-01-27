import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class NecnatAbpService {
  apiName = 'NecnatAbp';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/NecnatAbp/sample' },
      { apiName: this.apiName }
    );
  }
}
