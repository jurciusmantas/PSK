# Task description

## Problem

Every DevBridge employee anually can use fixed number of days to learn new work-related material (topic is selected freely.
On this time there is not any centralized system that can show which topics each employee has learned. For this reason team leaders 
struggle to ensure sharing of knowledge among colleagues or plan future works according to employees' skills.

## Solution

Team leaders want to see structurized information about topics, their relations, how many employees leaned particular topics recently.
Employees should be able to put links into used materials and write comments about their learning days. This would allow to distinguish 
more advanced colleagues or discover the lack of knowledge in particular topics or teams.

## Functional requirements
1. Employee should have a calendar. There should be presented past and future learning days and topics. Accessibility: employee and his team leader. 
2. Employee should have a possibility to add learning day according to restrictions (e.g. not used all month or year limits). Topic is seleceted from 
existing ones or by creating th new one.
3. Learning days should have comments and links into used materials. Employee can add.
4. Team leader should have separete calendar containing all team members learning days.
5. Managed restrictions - limits how many consecutive learning days employee can have, maximum per month and year. Restrictions should be global or 
unique for each employee.
6. Sort which shows which employee learnt partcular topic.
7. Sort which shows which teams learnt pasrticular topic (how many employees from the team).
8. Sort which shows topics that team learnt and is planning to learn.
9. Employee should be able to receive "goals" - recommendations to learn particular topics.
10. Employee should have a possibility to create new topics and split them into subtopics.
11. Employee should have a possibility to add additional information (links, reviews) for topic.
12. Visualization how learning tree could look like (https://coggle.it/diagram/WqgTTNMJtPiHph_q/t/java-development-in-2018).
13. Possibility in learning tree to see employees or teams who learnt particular topics and see their learning path in this tree.

## Extensions (optional)
1. Repeatable events for employee. E.g. possibility to create regular learning days on last month's Friday.
2. Employees could be separated into roles. E.g. (junior developer, senior developer).
3. Separate goals (recommendations) for employee, team or role.
4. Possibility to export data into particular format (e.g. CSV).
5. Event in calendar creation in Outlook app.

## Entities
1. Employee
2. Team leader
3. Team
4. Restriction
5. Topic
6. Subtopic
7. Goal (recommendation)
8. Learning day

# TSP notes

First meeting results. If anyone has complains, it is possible to correct.

**Till next Tuesday we all must know how to launch a project and know what it is working. ** 

## Start

### Goal

- Don't be late after deadlines.
- Carry out at least 75% additional requirements.

### Roles

| Role | Function | Memeber's name |
| ------------------------ | ----------------------------------------------------------------------- | -------- |
| **Team leader**          | Controls the worklflow, productivity and deadlines                      | Lukas    |
| **Planner**              | Creates a plan for next weeks                                           | Mantas   |
| **Engineer**             | Creates and develops architecture structure and design.                 | Andrius  |
| **Main tester**          | Controls that there are no bugs and all requirement are satisfied       | Rimvydas |
| **Quality tester**       | Enusures code quality, writes documentation and does additional works   | Toma     |

### Meetings

Meetings are organized in Mondays between lectures or Tuesdays after Ragaisis lecture.

## Strategy

Main cycles duration: 1 month. Total 3 cycles (March, April, May).

### I cycle

- Requirements
- Project
- Login
- Database entities
- Employee/Team/Restrictions registration
- Employee use cases

### II cycle

- Learning day input (everything related to that, e.g. topic creation)
- Rekomendations front-end part.
- Team-leader part.
- Different types of sorts (from docs)
- Style, improvement of front-end design. 

### III cycle

- Tree view.
- Additional tasks.
- Everything what we did not catch up in previous cycles.
