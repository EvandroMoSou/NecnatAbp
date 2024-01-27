import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { NecnatAbpComponent } from './components/necnat-abp.component';
import { NecnatAbpService } from '@necnat-abp';
import { of } from 'rxjs';

describe('NecnatAbpComponent', () => {
  let component: NecnatAbpComponent;
  let fixture: ComponentFixture<NecnatAbpComponent>;
  const mockNecnatAbpService = jasmine.createSpyObj('NecnatAbpService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [NecnatAbpComponent],
      providers: [
        {
          provide: NecnatAbpService,
          useValue: mockNecnatAbpService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NecnatAbpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
