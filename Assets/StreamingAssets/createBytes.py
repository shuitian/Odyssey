import xlrd
import sqlite3,os,time,json
import struct

filename = 'setting.bytes'
f = open(filename, "w");

data = xlrd.open_workbook("monsters.xlsx")
tables =  data.sheets()
for table in tables:
	if table.name == "setting":
		s = {}
		nrows = table.nrows
		for row in xrange(0,nrows):
			# print str(table.cell(row,0))
			# print table.cell(row,1)
			s[str(table.cell(row,0).value)] = int(table.cell(row,1).value)
		s = json.dumps(s)
		print s
		f.write(struct.pack(str(len(s))+'s',s))
		break