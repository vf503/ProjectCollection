select count(ProjectPlanId)
from ProjectPlan
where CONVERT(varchar(10),MakingDate,120) = CONVERT(varchar(10),getDate(),120) 