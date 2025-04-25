import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

//este guard solo nos ayuda con rutas de angular, no con cosas fuera como en el browser
export const preventUnsavedChangesGuard: CanDeactivateFn<MemberEditComponent> = (component) => {
  if(component.editForm?.dirty){
    return confirm('are you sure you want to continue? any unsaved changes will be lost')
  }
  return true
};
