import { ModuleWithProviders, NgModule } from '@angular/core';
import { NECNAT_ABP_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class NecnatAbpConfigModule {
  static forRoot(): ModuleWithProviders<NecnatAbpConfigModule> {
    return {
      ngModule: NecnatAbpConfigModule,
      providers: [NECNAT_ABP_ROUTE_PROVIDERS],
    };
  }
}
