select *,dd1.text as ProjectPlanTypeText,dd2.text as ProgressText 
,(select count(ProjectId) from Project where Project.ProjectPlanId=ProjectPlan.ProjectPlanId) 
as ProjectCount
,(select count(ProjectId) from Project where Project.ProjectPlanId=ProjectPlan.ProjectPlanId and Project.progress in ('00000000-0000-0000-0000-000000000119','00000000-0000-0000-0000-000000000128') ) 
as ProjectFinishCount
from ProjectPlan
left join data_dictionary as dd1 on dd1.dictionary_identity=ProjectPlan.ProjectPlanTypeId 
left join data_dictionary as dd2 on dd2.dictionary_identity=ProjectPlan.progress
order by MakingDate Desc