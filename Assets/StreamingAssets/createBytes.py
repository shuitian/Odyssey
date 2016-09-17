import xlrd
import sqlite3,os,time,json
import struct

data = xlrd.open_workbook("bytes.xlsx")
tables =  data.sheets()
for table in tables:
	filename = r"Bytes/"+table.name+".bytes"
	f = open(filename, "w");
	if table.name == "setting":
		s = {}
		nrows = table.nrows
		for row in xrange(0,nrows):
			# print str(table.cell(row,0))
			# print table.cell(row,1)
			s[str(table.cell(row,0).value)] = int(table.cell(row,1).value)
	else :
		s = []
		nrows = table.nrows
		ncols = table.ncols
		for row in xrange(1,nrows):
			dic = {}
			dic["data"] = {}
			for col in xrange(0,ncols):
				cell = table.cell(row,col).value
				if type(cell) is float:
					if float(int(cell)) == cell:
						txt = str(int(cell))
					else:
						txt = str(cell)
				elif type(cell) is unicode:
					txt = cell.encode("utf-8")
				key = str(table.cell(0,col).value)
				if key == "id":
					dic[key] = txt
				else :
					dic["data"][key] = txt
				# if col == 1:
				# 	break
			s.append(dic)
			# break
		# print s


		# for row in xrange(0,nrows):
		# 	# print str(table.cell(row,0))
		# 	# print table.cell(row,1)
		# 	s[str(table.cell(row,0).value)] = int(table.cell(row,1).value)
	s = json.dumps(s)
	print table.name+":"+s
	f.write(struct.pack(str(len(s))+'s',s))

	f.close()