import { Project } from '../models/project.model';
import { Skill } from '../models/skill.model';
import { Experience } from '../models/experience.model';
import { Education } from '../models/education.model';
import { Certification } from '../models/certification.model';

const GITHUB_PROFILE = 'https://github.com/yogaraj-1';

export const PROJECTS: Project[] = [
  {
    id: 1,
    title: 'Real-Time Chat Application',
    description:
      'A real-time chat application supporting instant communication between users using Socket.io and a MERN-based architecture.',
    longDescription:
      'Chatty is a real-time chat platform built with the MERN stack. Socket.io delivers messages instantly, JWT authentication protects user access, and secure REST APIs handle users and message history behind a responsive React interface.',
    technologies: ['MERN Stack', 'Socket.io', 'JWT'],
    category: 'Full Stack',
    features: [
      'Instant one-to-one messaging with Socket.io',
      'JWT authentication and protected REST APIs',
      'Online/offline status and contact sidebar',
      'Responsive React.js interface',
      'Scalable Node.js, Express.js and MongoDB backend'
    ],
    githubUrl: GITHUB_PROFILE,
    liveUrl: 'https://chat-application-2iok.onrender.com/',
    image: 'images/projects/chatty.jpg'
  },
  {
    id: 2,
    title: 'Tutorra — Online Learning Platform',
    description:
      'An online learning platform with authentication, course management, payments and cloud media handling.',
    longDescription:
      'Tutorra is a full-stack Learning Management System built with the MERN stack. It supports secure online learning, course creation, student enrollment and progress tracking, with Clerk authentication, Stripe payments and Cloudinary media uploads.',
    technologies: ['MERN', 'Clerk', 'Stripe', 'Cloudinary'],
    category: 'Full Stack',
    features: [
      'Clerk authentication',
      'Course creation and management',
      'Student enrollment and progress tracking',
      'Stripe payments',
      'Cloudinary media uploads',
      'Protected APIs and a responsive React.js frontend'
    ],
    githubUrl: GITHUB_PROFILE,
    liveUrl: 'https://tutorra-frontend-rfdp.onrender.com/',
    image: 'images/projects/tutorra.jpg'
  },
  {
    id: 3,
    title: 'AgriConnect',
    description:
      'A platform designed to connect farmers with digital services, marketplace features and AI-powered assistance.',
    longDescription:
      'AgriConnect is a platform that connects farmers with digital services. It combines marketplace features with AI-powered assistance, built with React, TypeScript, Node.js and MongoDB.',
    technologies: ['React', 'TypeScript', 'Node.js', 'MongoDB', 'AI'],
    category: 'AI',
    features: [
      'Digital services for farmers',
      'Marketplace features',
      'AI-powered assistance',
      'Typed React and Node.js codebase'
    ],
    githubUrl: GITHUB_PROFILE
  },
  {
    id: 4,
    title: 'Student Result Analysis',
    description:
      'A student result analysis application for managing and analyzing academic performance.',
    longDescription:
      'A Java application that manages student results and analyzes academic performance, using a database for storage and retrieval.',
    technologies: ['Java', 'Database'],
    category: 'Backend',
    features: [
      'Student result management',
      'Academic performance analysis',
      'Database-backed storage'
    ],
    githubUrl: GITHUB_PROFILE
  },
  {
    id: 5,
    title: 'Hospital Management System',
    description:
      'A hospital management system for handling patients, appointments and related hospital information.',
    longDescription:
      'A Java and JDBC application backed by MySQL for managing patients, appointments and related hospital information.',
    technologies: ['Java', 'JDBC', 'MySQL'],
    category: 'Backend',
    features: [
      'Patient records management',
      'Appointment handling',
      'JDBC database access with MySQL'
    ],
    githubUrl: GITHUB_PROFILE
  },
  {
    id: 6,
    title: 'Movie Ticket Booking System',
    description:
      'A C# and .NET application to manage movies, showtimes and ticket bookings, backed by SQL Server.',
    longDescription:
      'A console-based Movie Booking System built with C# and .NET to manage movies, showtimes and ticket bookings. Database operations use Entity Framework Core with SQL Server, with well-designed entity relationships and object-oriented principles organizing the application logic.',
    technologies: ['C#', '.NET', 'Entity Framework Core', 'SQL Server'],
    category: '.NET',
    features: [
      'Movie, showtime and ticket booking management',
      'Entity Framework Core with SQL Server',
      'Designed entity relationships',
      'Object-oriented application structure'
    ],
    githubUrl: 'https://github.com/yogaraj-1/HCL_Training/tree/main/Day18/MovieBookingSystem'
  }
];

// NOTE: "level" values are estimates for the progress bars — adjust them honestly.
export const SKILLS: Skill[] = [
  { id: 1, name: 'Java', category: 'Programming', level: 80 },
  { id: 2, name: 'Python', category: 'Programming', level: 65 },
  { id: 3, name: 'JavaScript', category: 'Programming', level: 85 },
  { id: 4, name: 'TypeScript', category: 'Programming', level: 70 },
  { id: 5, name: 'C#', category: 'Programming', level: 65 },
  { id: 6, name: 'HTML', category: 'Frontend', level: 90 },
  { id: 7, name: 'CSS', category: 'Frontend', level: 85 },
  { id: 8, name: 'JavaScript', category: 'Frontend', level: 85 },
  { id: 9, name: 'React.js', category: 'Frontend', level: 80 },
  { id: 10, name: 'Angular', category: 'Frontend', level: 70 },
  { id: 11, name: 'Node.js', category: 'Backend', level: 80 },
  { id: 12, name: 'Express.js', category: 'Backend', level: 80 },
  { id: 13, name: 'REST APIs', category: 'Backend', level: 85 },
  { id: 14, name: 'JWT', category: 'Backend', level: 75 },
  { id: 15, name: 'Socket.io', category: 'Backend', level: 70 },
  { id: 16, name: 'MongoDB', category: 'Database', level: 80 },
  { id: 17, name: 'MySQL', category: 'Database', level: 75 },
  { id: 18, name: 'SQL Server', category: 'Database', level: 70 },
  { id: 19, name: 'Git', category: 'Tools', level: 85 },
  { id: 20, name: 'GitHub', category: 'Tools', level: 85 },
  { id: 21, name: 'VS Code', category: 'Tools', level: 90 },
  { id: 22, name: 'IntelliJ IDEA', category: 'Tools', level: 80 },
  { id: 23, name: 'Visual Studio', category: 'Tools', level: 75 },
  { id: 24, name: 'Postman', category: 'Tools', level: 80 }
];

export const EXPERIENCES: Experience[] = [
  {
    id: 1,
    company: 'Profit Apps India Pvt. Ltd.',
    role: 'Software Development Intern',
    duration: 'Aug 2026 – Sept 2026',
    description:
      'Worked on Profit.co, fixing bugs and implementing minor features in an existing Angular-based product. Used Cursor AI for efficient debugging and coding, gaining experience with real-world codebases and corporate workflows.',
    technologies: ['Angular', 'Cursor AI']
  }
];

export const EDUCATION: Education[] = [
  {
    id: 1,
    institution: 'PSNA College of Engineering and Technology',
    degree: 'B.Tech Information Technology',
    year: '2023 – 2027',
    result: 'CGPA: 8.7 / 10'
  },
  {
    id: 2,
    institution: 'PKM HR SEC SCHOOL',
    degree: 'HSC Computer Science',
    year: '2023',
    result: '92%'
  }
];

export const CERTIFICATIONS: Certification[] = [
  {
    id: 1,
    title: 'Salesforce Certified Platform Developer',
    date: 'October 2026',
    url: 'https://drive.google.com/file/d/1L1jPlPXfb5YdL1V_dyqUe-0ngEhdBz39/view?usp=drivesdk'
  },
  {
    id: 2,
    title: 'Automation Developer Associate',
    date: 'July 2025',
    url: 'https://drive.google.com/file/d/1FofNsXoGmfKU7JM3OcGle48VTHhBxQvZ/view?usp=drivesdk'
  },
  {
    id: 3,
    title: 'JavaScript (HackerRank)',
    date: 'March 2025',
    url: 'https://www.hackerrank.com/certificates/a353bbb61e8a'
  },
  {
    id: 4,
    title: 'Prize Winner — Coding Competition "Stack and Conquer" (NDLI Club & GDG, PSNACET)',
    date: 'August 2025'
  }
];
