import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'skillLevel' })
export class SkillLevelPipe implements PipeTransform {
  transform(level: number): string {
    if (level >= 90) return 'Expert';
    if (level >= 70) return 'Advanced';
    if (level >= 50) return 'Intermediate';
    return 'Beginner';
  }
}
