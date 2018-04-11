if not exists (select 1 from sys.columns c join sys.tables t on c.object_id = t.object_id where t.name = 'Cliente' and c.name = 'versao')
begin
  alter table Cliente add versao rowversion
end

if not exists (select 1 from sys.columns c join sys.tables t on c.object_id = t.object_id where t.name = 'Item' and c.name = 'versao')
begin
  alter table Item add versao rowversion
end
