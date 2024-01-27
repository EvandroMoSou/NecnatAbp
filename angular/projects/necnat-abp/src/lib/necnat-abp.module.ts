import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NecnatAbpComponent } from './components/necnat-abp.component';
import { NecnatAbpRoutingModule } from './necnat-abp-routing.module';

@NgModule({
  declarations: [NecnatAbpComponent],
  imports: [CoreModule, ThemeSharedModule, NecnatAbpRoutingModule],
  exports: [NecnatAbpComponent],
})
export class NecnatAbpModule {
  static forChild(): ModuleWithProviders<NecnatAbpModule> {
    return {
      ngModule: NecnatAbpModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<NecnatAbpModule> {
    return new LazyModuleFactory(NecnatAbpModule.forChild());
  }
}
