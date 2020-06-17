import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PostedSelectedComponent } from './posted-selected.component';

describe('PostedSelectedComponent', () => {
  let component: PostedSelectedComponent;
  let fixture: ComponentFixture<PostedSelectedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PostedSelectedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PostedSelectedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
