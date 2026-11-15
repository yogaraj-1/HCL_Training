export interface Project {
  id: number;
  title: string;
  description: string;
  longDescription: string;
  technologies: string[];
  category: string;
  features: string[];
  githubUrl: string;
  liveUrl?: string;
  image?: string;
}
